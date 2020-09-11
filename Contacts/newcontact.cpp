#include "newcontact.h"
#include "ui_newcontact.h"

NewContact::NewContact(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::NewContact)
{
    ui->setupUi(this);
    this->setWindowTitle("Create Contact");
    this->setWindowIcon(QIcon(":/Icons/Icons/Programicon.png"));
    editMode = false;

}

NewContact::~NewContact()
{
    delete ui;
}

void NewContact::EditMode(Contact contact, int index)
{
    editMode = true;
    editModeIndex = index;
    ui->NameEdit->setText(contact.getName());
    ui->SurnameEdit->setText(contact.getSurname());
    ui->BirthdayEdit->setDate(contact.getBirthday());
    ui->PhoneEdit->setValue(contact.getPhone());
    ui->EmailEdit->setText(contact.getEmail());
    setWindowTitle("Edit Contact");
    ui->AddButton->setText("Edit");
}

void NewContact::CreateMode()
{
    ui->NameEdit->clear();
    ui->SurnameEdit->clear();
    ui->BirthdayEdit->clear();
    ui->PhoneEdit->clear();
    ui->EmailEdit->clear();
    editMode = false;
    setWindowTitle("New Contact");
    ui->AddButton->setText("Create");
}

void NewContact::on_AddButton_clicked()
{

    QString name = ui->NameEdit->text();
    QString surname = ui->SurnameEdit->text();
    QDate birthday = ui->BirthdayEdit->date();
    int phone = ui->PhoneEdit->value();
    QString email = ui->EmailEdit->text();
    //int yyyy = birthday.year() , mm = birthday.month() , dd = birthday.day() ;
    Contact a(name,surname,birthday,phone,email);
    if(!editMode)
    {
        emit(Sig(a));
    }
    else
    {
        emit(ModContact(a,editModeIndex));
    }
    this->hide();
}

