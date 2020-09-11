#include "generateno.h"

GenerateNo::GenerateNo()
{

}


int GenerateNo::isEven(int a)
{
    if(a%2==0)
    {
        return true;
    }
    return false;
}


int GenerateNo::abs(int a)
{
    if(a>0)
    {
        return a;
    }
    return -1*a;
}


int GenerateNo::generate(int seed, int mod, int multi)
{
    //Simple algorythm
    return abs((seed*multi)%mod);
}


int GenerateNo::generate(int mod)
{
    //Algorythm that uses variables from the date and time to seed a random number
    int seed = (345941*now.date().month()*now.time().second())+now.time().hour();
    if(isEven(seed))
    {
        seed = seed*10;
        seed+=1;
    }
    int multi = (now.date().year() / now.time().second())*88241;
    multi+=now.time().msec();
    multi+=var;
    var+=now.time().msec();
    if(!isEven(multi))
    {
        multi = multi*10;
        multi+=1;
    }
    return abs((seed*multi)%mod);
}
