//
//  PM5Controller.hpp
//  BLEScanner
//
//  Created by Rodrigo Savage on 3/4/17.
//  Copyright © 2017 Michael Lehman. All rights reserved.
//

#ifndef PM5Controller_hpp
#define PM5Controller_hpp

#include <stdio.h>
#import "BufferPM5.h"
#import "ErgData.h"
#import "StrokeData.h"
#endif /* PM5Controller_hpp */

class PM5Controller {
private:
    

public:
    CBufferPM5 mPM5Buffer;
    ErgData mErgData;
    StrokeData mStrokeData;
     //  = new CBufferPM5(NULL);
    PM5Controller();
    void readCharacteristic31();
    void readCharacteristic32();
    void readCharacteristic35();
    void readCharacteristic36();
};
