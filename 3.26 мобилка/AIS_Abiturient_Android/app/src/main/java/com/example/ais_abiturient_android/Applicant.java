package com.example.ais_abiturient_android;

public class Applicant {
    public int id;
    public String fullName;
    public String birthDay;
    public String phone;
    public String email;
    public String documentType;
    public String documentNumber;
    public String submissionDate;

    public Applicant(int id, String fullName, String birthDay, String phone,
                     String email, String documentType, String documentNumber, String submissionDate) {
        this.id = id;
        this.fullName = fullName;
        this.birthDay = birthDay;
        this.phone = phone;
        this.email = email;
        this.documentType = documentType;
        this.documentNumber = documentNumber;
        this.submissionDate = submissionDate;
    }
}
