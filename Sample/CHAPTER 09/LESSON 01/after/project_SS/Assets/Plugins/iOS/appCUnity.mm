#import <Foundation/Foundation.h>
#import "appCCloud.h"

extern void UnitySendMessage(const char *, const char *, int);

static BOOL setup = NO;             // starting setup
static BOOL setupResult = NO;       // setup result
static NSMutableArray *arrServiceId = [[NSMutableArray alloc] init];


char* MakeStringCopyAppc (const char* string)
{
	if (string == NULL)
		return NULL;
	
	char* res = (char*)malloc(strlen(string) + 1);
	strcpy(res, string);
	return res;
}


//////////////////////////////////////////////////
// Class for appCCloud Delegate (interface)
@interface appCCloudDelegate : NSObject
<
appCDelegate,
appCMatchAppDelegate
>
@end

//////////////////////////////////////////////////
// Class for appCCloud Delegate (implementation)
@implementation appCCloudDelegate

- (void)setupAppC:(NSString *)mk option:(NSUInteger)option
{
    // make thread
    dispatch_queue_t queue;
    queue = dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_LOW, 0);
    dispatch_async(queue, ^{
        dispatch_async(dispatch_get_main_queue(), ^ {

            // setup appC cloud
            [appCCloud setupAppCWithMediaKey:mk
                                      option:option];
        });
    });
}

- (void)finishedSetupAppC:(BOOL)succeed
{
    NSLog(@"finishedSetupAppC");
    setupResult = succeed;
    setup = YES;
    // Start Gamers
    [appCCloud gamersStartGame];
}

- (void)onGetMatchAppWithAppName:(NSString *)app_name
                     description:(NSString *)description
                         caption:(NSString *)caption
                            icon:(UIImage *)icon
                          banner:(UIImage *)banner
{
    NSLog(@"inAppPurchased");
}

- (void)inAppPurchased:(appCInAppPurchaseProduct *)productIdentifier
{
    NSLog(@"inAppPurchased");
}

- (void)inAppPurchaseRestored:(appCInAppPurchaseProduct *)productIdentifier
{
    NSLog(@"inAppPurchaseRestored");
}

- (void)inAppPurchaseRestoreCompleted
{
    NSLog(@"inAppPurchaseRestoreCompleted");
}

- (void)inAppPurchaseRestoreFailed:(NSError *)error
{
    NSLog(@"inAppPurchaseRestoreFailed");
}

- (void)getService:(NSString *)service_id
{
    NSLog(@"getService");
    [arrServiceId addObject:service_id];
}

@end


//////////////////////////////////////////////////
// Root view controller of Unity screen.
extern UIViewController *UnityGetGLViewController();

static appCCutinView *cutinView = nil;
static appCCloudDelegate *appc_cloud = nil;


#pragma mark Plug-in Functions

//////////////////////////////////////////////////
// Setup Media Key
extern "C" BOOL _setupAppCWithMediaKey(const char* mk,
                                       int opt_ad,
                                       int opt_gamers,
                                       int opt_push,
                                       int opt_data,
                                       int opt_purchase,
                                       int opt_reward
                                       ) {
    NSUInteger opt = 0;
    if (opt_ad) opt |= APPC_CLOUD_AD;
    if (opt_gamers) opt |= APPC_CLOUD_GAMERS;
    if (opt_push) opt |= APPC_CLOUD_PUSH;
    if (opt_data) opt |= APPC_CLOUD_DATA;
    if (opt_purchase) opt |= APPC_CLOUD_PURCHASE;
    
    appc_cloud = [[appCCloudDelegate alloc] init];
    [appCCloud setDelegate:appc_cloud];
    setup = NO;
    [appc_cloud setupAppC:[NSString stringWithUTF8String:mk]
                   option:opt];
    
    int count = 0;
    while (setup == NO) {
        [[NSRunLoop currentRunLoop] runUntilDate:[NSDate dateWithTimeIntervalSinceNow:0.5]];
        count ++;
        // loop until 0.5sec * 30 = 15sec
        if (count >= 30) {
            break;
        }
    }
    NSLog(@"*** _setupAppCWithMediaKey setup=%d, setupResult=%d", setup, setupResult);

    return setupResult;
}

//////////////////////////////////////////////////
// webview or storekit or cutin
extern "C" int _isForegroundAd() {
	return (int)([appCCloud showingCutin]);
}

//////////////////////////////////////////////////
// for appCCutinView
void hideAppCCutinView() {
    if (cutinView) {
        [cutinView removeFromSuperview];
        cutinView = nil;
    }
}

extern "C" void _showAppCCutinView() {
	hideAppCCutinView();
    UIViewController *rootViewController = UnityGetGLViewController();
    cutinView = [[[appCCutinView alloc] initWithViewController:rootViewController
                                                    closeBlock:^(id sender_) {
                                                        [cutinView removeFromSuperview];
                                                        cutinView = nil;
                                                    }
                  ] autorelease];
    [rootViewController.view addSubview:cutinView];
}

extern "C" void _hideAppCCutinView() {
	hideAppCCutinView();
}

extern "C" int _showingAppCCutin() {
	return (int)[appCCloud showingCutin];
}

extern "C" void _showAppCMatchAppView(double horizontal, double vertical) {
	[appCCloud matchAppShowBannerWithHorizontal:(appCHorizontal)horizontal
                                       vertical:(appCVertical)vertical];
}

extern "C" void _hideAppCMatchAppView() {
	[appCCloud matchAppHideBanner];
}


//////////////////////////////////////////////////
// for MatchApp
extern "C" void _startAppCMatchApp() {
    [appCCloud matchAppStartWithDelegate:appc_cloud];
}

extern "C" void _stopAppCMatchApp() {
    [appCCloud matchAppStop];
}


//////////////////////////////////////////////////
// for appCClud Gamers
extern "C" int _openGamers() {
    return (int)[appCCloud openGamers];
}

extern "C" int _gamersStartGame() {
    return (int)[appCCloud gamersStartGame];
}

extern "C" const char* _gamersGetNickname() {
    return [appCCloud gamersGetNickname].UTF8String;
}

extern "C" int _gamersPlayCountIncrement() {
    return (int)[appCCloud gamersPlayCountIncrement];
}

extern "C" int _gamersGetPlayCount() {
    return (int)[appCCloud gamersGetPlayCount];
}

extern "C" void _gamersAddActiveLb(const char* lb_ids[], int count) {
    NSMutableArray *activeLbs = [NSMutableArray array];
    for (int i=0; i<count; i++) {
        NSString *s = [NSString stringWithUTF8String:lb_ids[i]];
        [activeLbs addObject:s];
    }
    // set Active Leaderboards
    [appCCloud gamersSetActiveLbs:activeLbs];
}

extern "C" int _gamersAddLbInt(int lb_id, int score) {
    return (int)[appCCloud gamersAddLbWithId:lb_id
                                      scorei:score];
}

extern "C" int _gamersAddLbDec(int lb_id, float score) {
    return (int)[appCCloud gamersAddLbWithId:lb_id
                                      scored:score];
}

extern "C" char* _gamersGetLb(int lb_id) {
    return MakeStringCopyAppc([appCCloud gamersGetLbWithId:lb_id].UTF8String);
}


//////////////////////////////////////////////////
// for appCCloud Recovery
extern "C" void _startRecovery() {
    [appCCloud startRecovery];
}


//////////////////////////////////////////////////
// for appCCloud DataStore
extern "C" void _dataStoreAddActiveData(const char* data_ids[], int count) {
    NSMutableArray *activeDatas = [NSMutableArray array];
    for (int i=0; i<count; i++) {
        NSString *s = [NSString stringWithUTF8String:data_ids[i]];
        [activeDatas addObject:s];
    }
    // set Active DataStore
    [appCCloud dataStoreSetActiveKeys:activeDatas];
}

extern "C" int _dataStoreIntegerWithKey(const char* key) {
    return (int)[appCCloud dataStoreIntegerWithKey:[NSString stringWithUTF8String:key]];
}

extern "C" char* _dataStoreStringWithKey(const char* key) {
    NSString *ret = [appCCloud dataStoreStringWithKey:[NSString stringWithUTF8String:key]];
    return MakeStringCopyAppc(ret.UTF8String);
}

extern "C" int _dataStoreSetIntegerWithKey(const char* key, int value) {
    return (int)[appCCloud dataStoreSetIntegerWithKey:[NSString stringWithUTF8String:key]
                                              integer:value];
}

extern "C" int _dataStoreSetStringWithKey(const char* key, const char* value) {
    return (int)[appCCloud dataStoreSetStringWithKey:[NSString stringWithUTF8String:key]
                                              string:[NSString stringWithUTF8String:value]];
}

//////////////////////////////////////////////////
// for appCCloud InAppPurchase
extern "C" int _purchaseShowList() {
    return (int)[appCCloud purchaseShowList];
}

extern "C" int _purchaseRestore() {
    return (int)[appCCloud purchaseRestore];
}

extern "C" void _purchaseAddActiveItem(const char *key, const char *product_ids[], int count) {
    NSString *key0 = [NSString stringWithUTF8String:key];
    NSMutableArray *activeItems = [NSMutableArray array];
    for (int i=0; i<count; i++) {
        NSString *s = [NSString stringWithUTF8String:product_ids[i]];
        [activeItems addObject:s];
    }
    // set Active Items
    [appCCloud purchaseAddActiveKey:key0
                                ids:activeItems];
}

extern "C" int _purchaseUseCountWithKey(const char* key, int reduce) {
    return (int)[appCCloud purchaseUseCountWithKey:[NSString stringWithUTF8String:key]
                                            reduce:reduce];
}

extern "C" int _purchaseSetCountWithKey(const char* key, int count) {
    return (int)[appCCloud purchaseSetCountWithKey:[NSString stringWithUTF8String:key]
                                             count:count];
}

extern "C" int _purchaseGetCountWithKey(const char* key) {
    return (int)[appCCloud purchaseGetCountWithKey:[NSString stringWithUTF8String:key]];
}

extern "C" char* _purchaseGetJsonItem(const char* key) {
    NSString *ret = @"";
    NSString *key0 = [NSString stringWithUTF8String:key];
    NSArray *items = [appCCloud purchaseGetAllData];
    for (NSDictionary *item in items) {
        NSString *category_key = [item objectForKey:@"category_key"];
        if ([key0 isEqualToString:category_key]) {
            // make JSON string
            NSString *category_name = [item objectForKey:@"category_name"];
            NSNumber *current_count = [item objectForKey:@"current_count"];
            NSMutableDictionary *ret_item = [NSMutableDictionary dictionary];
            [ret_item setValue:category_key forKey:@"id"];
            [ret_item setValue:category_name forKey:@"name"];
            [ret_item setValue:current_count forKey:@"count"];
            NSData *data = [NSJSONSerialization dataWithJSONObject:ret_item
                                                           options:NSJSONWritingPrettyPrinted
                                                             error:nil];
            ret = [[NSString alloc] initWithData:data
                                        encoding:NSUTF8StringEncoding];
            break;
        }
    }
    return MakeStringCopyAppc(ret.UTF8String);
}
