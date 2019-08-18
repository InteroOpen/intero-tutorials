//
//  BufferPM5.cpp
//  BLEScanner
//
//  Created by Rodrigo Savage on 3/1/17.
//

#include "BufferPM5.h"

CBufferPM5::CBufferPM5(void* array){
    this->data = (unsigned char*)array;
    index = 0;
}
void CBufferPM5::setByteArray(void* array){
    this->data = (unsigned char*)array;
    index = 0;
}

float CBufferPM5::readByte(){
    Bitfield* b = (Bitfield*)(data + index);
    uint32_t a = b->uint8;
    index+= 1;
    return a*1.0f;
}
char* CBufferPM5::getDebugByteString(){
    static char buff[20];
    sprintf(buff, "%x %x %x | %x %x %x | %x %x\n",data[0],data[1],data[2],data[3],data[4],data[5],data[6],data[7]);
    return buff;
}
float CBufferPM5::read2Bytes(){
//    printf("%x %x\n",data[index],data[index+1]);
    float value = (data[index] | data[index+1] <<8 );
    index+= 2;
    return value;
   /*
    Bitfield* b = (Bitfield*)(data + index);
    uint32_t a = b->uint16;
    index+= 2;
    return a*1.0f;
    this.index = this.index + 3;
    return (data[index] | data[index+1]; <<8 |  data[index+2] << 16)*multiplier;*/
    
    
}
float CBufferPM5::read3Bytes(){
    /*Bitfield* b = (Bitfield*)(data + index);
    uint32_t a = b->uint24;
    */
    float value = (data[index] | data[index+1] <<8 |  data[index+2] << 16);
    index+= 3;
    return value;
    
//    index+= 3;
//    return a*1.0f;
}
float CBufferPM5::readTime(){
    return read3Bytes()*0.01f;
}
float CBufferPM5::readDistance(){
    return this->read3Bytes()*0.1f;
}
float CBufferPM5::readPace(){
    return read2Bytes()*0.01f;
}
float CBufferPM5::readUnimportantFlags(int nFlags){
    index = index + nFlags;
    return 0.0f;
};
