#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    readSettings();
    ui->tableView->setSortingEnabled(true);
    ui->tableView->setEditTriggers(QAbstractItemView::NoEditTriggers);
    ui->tableView->horizontalHeader()->setStretchLastSection(true);
    this->setWindowTitle("Contacts");
    this->setWindowIcon(QIcon(":/Icons/Icons/Programicon.png"));
    itemModel = new QStandardItemModel(0,4);
    newContact = new NewContact();
    newContact->hide();

    connect(newContact,SIGNAL(Sig(Contact)),this,SLOT(Slurt(Contact)));
    connect(newContact,SIGNAL(ModContact(Contact,int)),this,SLOT(modifyContact(Contact,int)));

    ui->AddButton->setToolTip(tr("Create a new contact"));
    ui->DeleteButton->setToolTip(tr("Remove the selected contact"));
    ui->EditButton->setToolTip(tr("Edit the selected contact's details"));
    ui->LogOutButton->setToolTip(tr("Log out of database"));

    ui->AddButton->setIcon(QIcon(":/Icons/Icons/edit.png"));
    ui->EditButton->setIcon(QIcon(":/Icons/Icons/edit.png"));
    ui->DeleteButton->setIcon(QIcon(":/Icons/Icons/delete.png"));
    ui->LogOutButton->setIcon(QIcon(":/Icons/Icons/logout.png"));

    ui->AddButton->setText(tr(" Add"));
    ui->EditButton->setText(tr(" Edit"));
    ui->DeleteButton->setText(tr(" Delete"));
    ui->LogOutButton->setText(tr(" Logout"));


    //New <div>Icons made by <a href="https://www.flaticon.com/authors/freepik" title="Freepik">Freepik</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a></div>
    //Edit icon made by <a href="https://www.flaticon.com/authors/those-icons" title="Those Icons">Those Icons</a> from <a href="https://www.flaticon.com/" title="Flaticon"> www.flaticon.com</a>
    //delete Icons made by <a href="https://www.flaticon.com/authors/freepik" title="Freepik">Freepik</a> from <a href="https://www.flaticon.com/" title="Flaticon"> www.flaticon.com</a>
    //logout Icons made by <a href="https://www.flaticon.com/authors/dmitri13" title="dmitri13">dmitri13</a> from <a href="https://www.flaticon.com/" title="Flaticon"> www.flaticon.com</a>


    updateVeiw();
}

MainWindow::~MainWindow()
{
    delete ui;
    delete newContact;

}

int MainWindow::getNoContacts()
{
    return Contacts.size();
}

void MainWindow::on_AddButton_clicked()
{
    newContact->CreateMode();
    newContact->show();
}

void MainWindow::Slurt(Contact contact)
{
    Contacts.append(contact);
    updateVeiw();
    emit(sig(contact));
}

void MainWindow::addContact(Contact contact)
{
    Contacts.append(contact);
    updateVeiw();
}

void MainWindow::modifyContact(Contact contact, int index)
{
    Contacts[index]=contact;

    emit(deleteAll());

    //add items to database this leaves out the removed item.
    for(int i = 0 ; i < Contacts.size() ; i++)
    {
        emit(addContacts(Contacts[i],i+1));
    }
    updateVeiw();
}

void MainWindow::resizeEvent(QResizeEvent *event)
{
    QMainWindow::resizeEvent(event);
    ui->tableView->setColumnWidth(0,this->width()/5);
    ui->tableView->setColumnWidth(1,this->width()/5);
    ui->tableView->setColumnWidth(2,this->width()/7);
    ui->tableView->setColumnWidth(3,this->width()/7);
    ui->tableView->setColumnWidth(4,this->width()/5);

}

void MainWindow::closeEvent(QCloseEvent *event)
{
    QSettings settings("Contacts","");
    settings.setValue("geometry",saveGeometry());
    settings.setValue("windowState",saveState());
    QMainWindow::closeEvent(event);
}

void MainWindow::readSettings()
{
        QSettings settings("Contacts","");
        restoreGeometry(settings.value("MainWindow/geometry").toByteArray());
        restoreState(settings.value("MainWindow/windowState").toByteArray());
}


void MainWindow::updateVeiw()
{

    int size = Contacts.size();
    itemModel->clear();
    for(int i = 0 ; i < size ; i++)
    {
        QList <QStandardItem* > items;
        items.append(new QStandardItem(Contacts[i].getName()));
        items.append(new QStandardItem(Contacts[i].getSurname()));
        items.append(new QStandardItem(Contacts[i].getBirthday().toString()));
        items.append(new QStandardItem(QString::number(Contacts[i].getPhone())));
        items.append(new QStandardItem(Contacts[i].getEmail()));
        itemModel->appendRow(items);

    }
    QStringList headerLabels;
    headerLabels<<tr("Name")<<tr("Surname")<<tr("Date")<<tr("Phone")<<tr("Email");
    itemModel->setHorizontalHeaderLabels(headerLabels);


    ui->tableView->setModel(itemModel);
}



void MainWindow::on_DeleteButton_clicked()
{
    //Poor implementation
    int row = ui->tableView->currentIndex().row();
    Contacts.removeAt(row);


//    QModelIndexList indexes = ui->tableView->selectionModel()->selection().indexes();
//    for(int i = indexes.first().row() ; i<indexes.first().row()+indexes.count(); i++)
//    {
//        Contacts.removeAt(i);
//    }


    //delete database
    emit(deleteAll());

    //add items to database this leaves out the removed item.
    for(int i = 0 ; i < Contacts.size() ; i++)
    {
        emit(addContacts(Contacts[i],i+1));
    }
    updateVeiw();
}

void MainWindow::on_EditButton_clicked()
{
    //Connect edit with signals and slot this will change database. Poor implementation


    if(ui->tableView->currentIndex().isValid())
    {
        int index = ui->tableView->currentIndex().row();
        newContact->EditMode(Contacts[index],index);
        newContact->show();
    }

}

void MainWindow::on_LogOutButton_clicked()
{
    hide();
    emit(hideui());
}
