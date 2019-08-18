//
//  ErgData.hpp
//  BLEScanner
//
//  Created by Rodrigo Savage on 3/8/17.
//  Copyright © 2017 Michael Lehman. All rights reserved.
//

#ifndef ErgData_hpp
#define ErgData_hpp

#include <stdio.h>
class ErgData {
public:
    float time;
    float distance;
    float power;
    float pace;
    float spm;
    float calhr;
    float calories;
    char* ToString();
};
#endif /* ErgData_hpp */
