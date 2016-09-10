#import "appCCloud.h"
#import "appCCloudPushHandlerInternal.h"
#import <objc/runtime.h>

@implementation UIApplication(appCCloudPushHandlerInternal)

+(void)load
{
    method_exchangeImplementations(class_getInstanceMethod(self,
                                                           @selector(setDelegate:)),
                                   class_getInstanceMethod(self,
                                                           @selector(appCCloudAppDelegate:)));
	UIApplication *app = [UIApplication sharedApplication];
	NSLog(@"Init app: %@, %@", app, app.delegate);
}

void appCCloudRunTimeDidRegisterForRemoteNotificationsWithDeviceToken(id self, SEL _cmd, id application, id devToken)
{
    [appCCloud pushNotificationDidRegisterWithDeviceToken:devToken];
}

void appCCloudRunTimeDidFailToRegisterForRemoteNotificationsWithError(id self, SEL _cmd, id application, id error)
{
    [appCCloud pushNotificationDidFailWithError:error];
}

void appCCloudRunTimeDidReceiveRemoteNotification(id self, SEL _cmd, id application, id userInfo)
{
    [appCCloud pushNotificationDidReceive:userInfo appStat:((UIApplication*)application).applicationState];
}


static void exchangeMethodImplementations(Class class, SEL oldMethod, SEL newMethod, IMP impl, const char * signature)
{
	Method method = nil;
	method = class_getInstanceMethod(class, oldMethod);
	
	if (method)
    {
		class_addMethod(class, newMethod, impl, signature);
		method_exchangeImplementations(class_getInstanceMethod(class, oldMethod),
                                       class_getInstanceMethod(class, newMethod));
	}
    else
    {
		class_addMethod(class, oldMethod, impl, signature);
	}
}

- (void) appCCloudAppDelegate:(id<UIApplicationDelegate>)delegate
{
    
	static Class delegateClass = nil;
	
	if(delegateClass == [delegate class])
	{
		[self appCCloudAppDelegate:delegate];
		return;
	}
	
	delegateClass = [delegate class];
    
    exchangeMethodImplementations(delegateClass, @selector(application:didRegisterForRemoteNotificationsWithDeviceToken:),
		   @selector(application:appCCloudDidRegisterForRemoteNotificationsWithDeviceToken:),
                                  (IMP)appCCloudRunTimeDidRegisterForRemoteNotificationsWithDeviceToken, "v@:::");
    
	exchangeMethodImplementations(delegateClass, @selector(application:didFailToRegisterForRemoteNotificationsWithError:),
		   @selector(application:appCCloudDidFailToRegisterForRemoteNotificationsWithError:),
                                  (IMP)appCCloudRunTimeDidFailToRegisterForRemoteNotificationsWithError, "v@:::");
    
	exchangeMethodImplementations(delegateClass, @selector(application:didReceiveRemoteNotification:),
		   @selector(application:appCCloudDidReceiveRemoteNotification:),
                                  (IMP)appCCloudRunTimeDidReceiveRemoteNotification, "v@:::");
    
	[self appCCloudAppDelegate:delegate];
}

@end
