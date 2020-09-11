#include "fileio.h"

fileIO::fileIO()
{

}

void fileIO::SaveFile(QWidget *parent, QString text)
{
    QString fileName = QFileDialog::getSaveFileName(parent,
        QObject::tr("Save partial header file"), "",
        QObject::tr("Header file (*.ph);;All Files (*)"));
    QFile file(fileName);
    if (file.open(QIODevice::WriteOnly))
    {
        QTextStream output(&file);
        output<<text;
        file.close();
    }
}

