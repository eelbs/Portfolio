#include "generateheader.h"

GenerateHeader::GenerateHeader()
{

}

const QString GenerateHeader::getFileText()
{
    return "private: \n" + priv + " \n public: \n" + pub;
}

QString GenerateHeader::capFirst(QString s)
{
    QString ret;
    ret = s.at(0);
    ret = ret.toUpper();
    for(int i = 1 ; i < s.size();i++)
    {
        ret.append(s.at(i));
    }
    return ret;
}

void GenerateHeader::add(QString type, QString name, QString parameterName)
{
    QStringList publist;
    QStringList privlist;

    privlist <<"      "<< type <<name <<"; \n" ;

    foreach (QString part , privlist)
    {
        priv.append(part + " ");
    }



    //Add to type and name to public function

    //Getter
    //const QString getName(){return name;}
    publist<<"      "<<"const"<<type<<QString("get"+capFirst(name))<<"(){ return "<<name<<" ; }\n";

    //Setter
    //void setName(){name = paramaterName;}
    publist<<"      "<<"void"<<QString("set"+capFirst(name))<<"("<<type<<parameterName<<"){"<<name<<"="<<parameterName<<" ; }\n";

    foreach (QString part, publist)
    {
        pub.append(part+" ");
    }


}


