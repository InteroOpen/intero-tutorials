//
//  StrokeData.cpp
//  BLEScanner
//
//  Created by Rodrigo Savage on 3/8/17.
//  Copyright © 2017 Michael Lehman. All rights reserved.
//

#include "StrokeData.h"

char* StrokeData::ToString(){
    static char buff[64];
    sprintf(buff,"%f,%f,%f,%f,%f,%f,%f,%f,%f\n",time,distance,driveLength,driveTime,strokeRecoveryTime,strokeRecoveryDistance,peakDriveForce,avgDriveForce,strokeCount);
    return buff;
}
