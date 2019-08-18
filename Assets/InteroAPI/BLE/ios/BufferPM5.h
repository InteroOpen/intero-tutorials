//
//  BufferPM5.hpp
//  BLEScanner
//
//  Created by Rodrigo Savage on 3/1/17.
//

#ifndef BufferPM5_hpp
#define BufferPM5_hpp

#include <stdio.h>
#include <stdint.h>

union Bitfield{
    uint8_t uint8;
    uint16_t uint16;
    uint32_t uint24 : 24;
    uint32_t uint32;
};

class CBufferPM5 {
private:
    CBufferPM5();
    uint8_t* data;
    int index;
public:
    void setByteArray(void* array);
    CBufferPM5(void* array);
    char* getDebugByteString();
    float readByte();
    float read2Bytes();
    float read3Bytes();
    float readTime();
    float readDistance();
    float readPace();
    float readUnimportantFlags(int sa);
};

#endif /* BufferPM5_hpp */
