#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

@interface UIApplication(SupressWarnings)
- (void)application:(UIApplication *)application appCCloudDidRegisterForRemoteNotificationsWithDeviceToken:(NSData *)devToken;
- (void)application:(UIApplication *)application appCCloudDidFailToRegisterForRemoteNotificationsWithError:(NSError *)err;
- (void)application:(UIApplication *)application appCCloudDidReceiveRemoteNotification:(NSDictionary *)userInfo;
@end
