package com.example.ais_abiturient_android;

import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.widget.Button;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;

public class OperatorActivity extends AppCompatActivity {

    RecyclerView recyclerView;
    ApplicantAdapter adapter;
    ArrayList<Applicant> applicantList = new ArrayList<>();
    SQLiteDatabase db;
    DatabaseHelper dbHelper;
    Button buttonAdd;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_operator);

        recyclerView = findViewById(R.id.recyclerView);
        buttonAdd = findViewById(R.id.buttonAdd);

        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        adapter = new ApplicantAdapter(applicantList);
        recyclerView.setAdapter(adapter);

        dbHelper = new DatabaseHelper(this);
        dbHelper.copyDatabaseIfNeeded();
        db = dbHelper.getReadableDatabase();

        loadApplicants();

        buttonAdd.setOnClickListener(v -> {
            startActivity(new Intent(this, ApplicantActivity.class));
        });
    }

    @Override
    protected void onResume() {
        super.onResume();
        loadApplicants(); // обновление после добавления
    }

    private void loadApplicants() {
        applicantList.clear();

        Cursor cursor = db.rawQuery("SELECT id, full_name, birth_day, phone, email, document_type, document_number, submission_date FROM applicants", null);
        while (cursor.moveToNext()) {
            int id = cursor.getInt(0);
            String fullName = cursor.getString(1);
            String birthDay = cursor.getString(2);
            String phone = cursor.getString(3);
            String email = cursor.getString(4);
            String documentType = cursor.getString(5);
            String documentNumber = cursor.getString(6);
            String submissionDate = cursor.getString(7);

            applicantList.add(new Applicant(id, fullName, birthDay, phone, email, documentType, documentNumber, submissionDate));
        }
        cursor.close();
        adapter.notifyDataSetChanged();
    }

}
