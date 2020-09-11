#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);



    QString h = gen.getFileText();

    QStrListModelType = new QStringListModel;
    QStrListModelName = new QStringListModel;
    QStrListModelParmaterName = new QStringListModel;

    QStrListModelType->setStringList(StrListType);
    QStrListModelName->setStringList(StrListName);
    QStrListModelParmaterName->setStringList(StrListParamaterName);


    ui->listView->setModel(QStrListModelType);
    ui->listView_2->setModel(QStrListModelName);
    ui->listView_3->setModel(QStrListModelParmaterName);
    ui->listView->setEditTriggers(QAbstractItemView::NoEditTriggers);
    ui->listView_2->setEditTriggers(QAbstractItemView::NoEditTriggers);
    ui->listView_3->setEditTriggers(QAbstractItemView::NoEditTriggers);



    setWindowTitle("HeaderGenerator");
    updateUi();
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_pushButton_clicked()
{

    if(!ui->lineEdit->text().isEmpty() && !ui->lineEdit_2->text().isEmpty())
    {
        StrListType.append(ui->lineEdit->text());
        StrListName.append(ui->lineEdit_2->text());
        if(ui->lineEdit_3->text().isEmpty())
        {
            StrListParamaterName.append("i");
        }
        else
        {
            StrListParamaterName.append(ui->lineEdit_3->text());
        }
    }
    updateUi();
}

void MainWindow::on_pushButton_2_clicked()
{
    if(ui->listView->currentIndex().row()>=0) //Ensures a row is selected.
    {
        QMessageBox msg;
        msg.setWindowTitle("Delete?");
        QString txt;
        txt.append("Delete this variable: ");
        txt.append(StrListName.at(ui->listView->currentIndex().row()));
        msg.setText(txt);
        msg.setStandardButtons(QMessageBox::Yes);
        msg.addButton(QMessageBox::No);
        msg.setDefaultButton(QMessageBox::Yes);
        if(msg.exec()==QMessageBox::Yes)
        {
            StrListType.removeAt(ui->listView->currentIndex().row());
            StrListName.removeAt(ui->listView->currentIndex().row());
            StrListParamaterName.removeAt(ui->listView->currentIndex().row());
        updateUi();
        }
    }
}

void MainWindow::on_pushButton_3_clicked()
{
    updateUi();
}


void MainWindow::updateUi()
{
     QStrListModelType->setStringList(StrListType);
     QStrListModelName->setStringList(StrListName);
     QStrListModelParmaterName->setStringList(StrListParamaterName);
}

void MainWindow::on_pushButton_4_clicked()
{
    ui->textBrowser->clear();
    if(StrListType.size()==StrListName.size())
    {
        for(int i = 0 ; i< StrListType.size() ; i++)
        {
            gen.add(StrListType.at(i),StrListName.at(i),StrListParamaterName.at(i));
        }
    }
    ui->textBrowser->append(gen.getFileText());
}

void MainWindow::on_pushButton_5_clicked()
{
    file.SaveFile(this,gen.getFileText());
}
