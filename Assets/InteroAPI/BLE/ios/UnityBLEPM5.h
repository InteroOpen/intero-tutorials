
//UnitySendMessage("BLEPM5Receiver", "onBLEInitilized", "OK");

#import <Foundation/Foundation.h>
#import <CoreBluetooth/CoreBluetooth.h>
#import <UIKit/UIKit.h>
#import "PM5Controller.h"

extern "C" {
    void connectToPM5(int s);
    void scanForPM5();
    StrokeData* readStrokeData();
    ErgData* readErgData();
    // read data direcly from buffer
    void changeChannel(int channel);
    float getTime();
    float getDistance();
    float getPower();
    float getPace();
    float getSPM();
    float getCalhr();
    float getCalories();
    
    float getDriveLength();
    float getDriveTime();
    float getStrokeRecoveryTime();
    float getStrokeRecoveryDistance();
    float getPeakDriveForce();
    float getAvgDriveForce();
    float getStrokeCount();
	// 36
	float getStrokePower();
	float getStrokeCalories();
	float getWorkPerStroke();
}

@interface UnityBLEPM5 : NSObject <CBCentralManagerDelegate,
CBPeripheralDelegate>


    // CBCentralManager *centralManager;
    // NSMutableDictionary *_peripherals;
    // NSMutableArray *_backgroundMessages;
    // BOOL _isPaused;
    // BOOL _alreadyNotified;
    // BOOL _isInitializing;
    // BOOL _rssiOnly;
//-(StrokeData*) strokeData ;
-(PM5Controller*) pm5Controller ;
-(char*) ch31;
-(char*) ch32;
-(char*) ch35;
-(char*) ch36;
//        - (void)insertObject:(id)anObject atIndex:(NSUInteger)index;
- (void)initialize:(char*)addr31 :(char*)addr32 :(char*) addr35 :(char*) addr36;
- (void)startScan;
- (void)disconnect;

// - (void)insertObject:(id)anObject atIndex:(NSUInteger)index;
/*
//@property (atomic, strong) NSMutableDictionary *_peripherals;
//@property (atomic) BOOL _rssiOnly;

- (void)initialize;
- (void)deInitialize;
- (void)scanForPeripheralsWithServices:(NSArray *)serviceUUIDs options:(NSDictionary *)options;
- (void)stopScan;
- (void)retrieveListOfPeripheralsWithServices:(NSArray *)serviceUUIDs;
- (void)connectToPeripheral:(NSString *)name;
- (void)disconnectPeripheral:(NSString *)name;
- (CBCharacteristic *)getCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString;
- (void)readCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString;
- (void)writeCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString data:(NSData *)data withResponse:(BOOL)withResponse;
- (void)subscribeCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString;
- (void)unsubscribeCharacteristic:(NSString *)name service:(NSString *)serviceString characteristic:(NSString *)characteristicString;

#ifdef TARGET_OS_IOS
- (void)peripheralName:(NSString *)newName;
- (void)createService:(NSString *)uuid primary:(BOOL)primary;
- (void)removeService:(NSString *)uuid;
- (void)removeServices;
- (void)createCharacteristic:(NSString *)uuid properties:(CBCharacteristicProperties)properties permissions:(CBAttributePermissions)permissions value:(NSData *)value;
- (void)removeCharacteristic:(NSString *)uuid;
- (void)removeCharacteristics;
- (void)startAdvertising;
- (void)stopAdvertising;
- (void)updateCharacteristicValue:(NSString *)uuid value:(NSData *)value;
#endif

- (void)pauseMessages:(BOOL)isPaused;
- (void)sendUnityMessage:(BOOL)isString message:(NSString *)message;

+ (NSString *) base64StringFromData:(NSData *)data length:(int)length;
*/
@end
/*
 
 @interface UnityMessage : NSObject
 
 {
 BOOL _isString;
 NSString *_message;
 }
 
 - (void)initialize:(BOOL)isString message:(NSString *)message;
 - (void)deInitialize;
 - (void)sendUnityMessage;
 
 @end
 */
