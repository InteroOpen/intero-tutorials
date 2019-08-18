//
//  ErgData.cpp
//  BLEScanner
//
//  Created by Rodrigo Savage on 3/8/17.
//  Copyright © 2017 Michael Lehman. All rights reserved.
//

#include "ErgData.h"

char* ErgData::ToString(){
    static char buff[64];
    
    sprintf(buff, "%f, %f, %f, %f, %f, %f, %f\n",distance,power,pace,spm,time,calhr,calories);
    return buff;
}
