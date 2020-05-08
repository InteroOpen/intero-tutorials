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
    // 35
    float time;
    float distance;
    float driveLength;
    float driveTime;
    float strokeRecoveryTime;
    float strokeRecoveryDistance;
    float peakDriveForce;
    float avgDriveForce;
    float strokeCount;
    // 36
    float strokePower;
    float strokeCalories;
    float workPerStroke;
    char* ToString();
};
#endif /* StrokeData_hpp */
