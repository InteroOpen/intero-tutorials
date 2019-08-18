//
//  StrokeData.hpp
//  BLEScanner
//
//  Created by Rodrigo Savage on 3/8/17.
//  Copyright © 2017 Michael Lehman. All rights reserved.
//

#ifndef StrokeData_hpp
#define StrokeData_hpp

#include <stdio.h>
class StrokeData{
public:
    float time;
    float distance;
    float driveLength;
    float driveTime;
    float strokeRecoveryTime;
    float strokeRecoveryDistance;
    float peakDriveForce;
    float avgDriveForce;
    float strokeCount;
    char* ToString();
};
#endif /* StrokeData_hpp */
