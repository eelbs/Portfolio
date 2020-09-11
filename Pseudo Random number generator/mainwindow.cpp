#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    ui->spinBox->setRange(0,2147483647);
    ui->spinBox->setValue(100);
    ui->spinBox_2->setRange(0,1024);
    ui->spinBox_2->setValue(16);
    this->setWindowTitle("Random");


    //Tests for the random number algorythm
    //This shows the average of the generated numbers are around 50 if the range is 100.
    /*
    QList<int> lst;
    for(int i =0;i<10000000;i++)
    {
        lst.append(nogen.generate(101));
    }
    QMessageBox msg;
    double val= 0;
    int sum = 0;
    for(int i = 0;i<lst.size();i++)
    {
        sum+=lst.at(i);
    }
    double stddiv;
    double sum2 = 0;
    for(int i = 0; i<lst.size();i++)
    {
        sum2+=(50-lst.at(i))*(50-lst.at(i));
    }
    stddiv  = sqrt(sum2/(lst.size()-1));




    val = static_cast<double>(sum)/static_cast<double>(lst.size());
    QString txt;
    txt = QString("Arithmetic average: ") + QString::number(val)+"\n";
    txt.append(QString("Standard of deviation: ")+QString::number(stddiv));
    msg.setText(txt);

    msg.exec();
    */
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_pushButton_clicked()
{
    //Generate Number
    QString txt;
    int no = nogen.generate(ui->spinBox->value()+1);
    txt = QString::number(no);
    ui->textBrowser->append(txt);
}

void MainWindow::on_pushButton_2_clicked()
{
    //Generate Pass
    QString pass;
    pass = txtgen.generatePass(ui->spinBox_2->value());
    ui->textBrowser_2->append(pass);
}

void MainWindow::on_pushButton_3_clicked()
{
    //Clear random no
    ui->textBrowser->clear();
}

void MainWindow::on_pushButton_4_clicked()
{
    //Clear random text
    ui->textBrowser_2->clear();
}
