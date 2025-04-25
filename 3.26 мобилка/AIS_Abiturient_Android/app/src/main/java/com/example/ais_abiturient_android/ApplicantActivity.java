package com.example.ais_abiturient_android;

import android.app.DatePickerDialog;
import android.content.ContentValues;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.widget.*;
import androidx.appcompat.app.AppCompatActivity;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

public class ApplicantActivity extends AppCompatActivity {

    EditText editTextFullName, editTextBirthDay, editTextPhone, editTextEmail,
            editTextDocumentNumber, editTextSubmissionDate;
    Spinner spinnerDocumentType;
    Button buttonSave;
    SQLiteDatabase db;
    int applicantId = -1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_applicant);

        editTextFullName = findViewById(R.id.editTextFullName);
        editTextBirthDay = findViewById(R.id.editTextBirthDay);
        editTextPhone = findViewById(R.id.editTextPhone);
        editTextEmail = findViewById(R.id.editTextEmail);
        spinnerDocumentType = findViewById(R.id.spinnerDocumentType);
        editTextDocumentNumber = findViewById(R.id.editTextDocumentNumber);
        editTextSubmissionDate = findViewById(R.id.editTextSubmissionDate);
        buttonSave = findViewById(R.id.buttonSave);

        db = new DatabaseHelper(this).getReadableDatabase();

        // Настройка выпадающего списка
        String[] documentTypes = {
                "Паспорт",
                "Свидетельство о рождении",
                "Загранпаспорт",
                "Военный билет",
                "Другой"
        };

        ArrayAdapter<String> adapter = new ArrayAdapter<>(
                this,
                android.R.layout.simple_spinner_item,
                documentTypes
        );
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerDocumentType.setAdapter(adapter);

        // Календарь
        editTextBirthDay.setFocusable(false);
        editTextBirthDay.setOnClickListener(v -> showDatePicker(editTextBirthDay));

        editTextSubmissionDate.setFocusable(false);
        editTextSubmissionDate.setOnClickListener(v -> showDatePicker(editTextSubmissionDate));

        // Установка сегодняшней даты по умолчанию
        if (!getIntent().hasExtra("id")) {
            String today = new SimpleDateFormat("yyyy-MM-dd").format(new Date());
            editTextSubmissionDate.setText(today);
        }

        if (getIntent().hasExtra("id")) {
            applicantId = getIntent().getIntExtra("id", -1);
            loadApplicant(applicantId);
        }

        buttonSave.setOnClickListener(v -> saveApplicant());
    }

    private void loadApplicant(int id) {
        Cursor cursor = db.rawQuery(
                "SELECT full_name, birth_day, phone, email, document_type, document_number, submission_date " +
                        "FROM applicants WHERE id = ?",
                new String[]{String.valueOf(id)}
        );
        if (cursor.moveToFirst()) {
            editTextFullName.setText(cursor.getString(0));
            editTextBirthDay.setText(cursor.getString(1));
            editTextPhone.setText(cursor.getString(2));
            editTextEmail.setText(cursor.getString(3));

            String docType = cursor.getString(4);
            ArrayAdapter<String> adapter = (ArrayAdapter<String>) spinnerDocumentType.getAdapter();
            int position = adapter.getPosition(docType);
            spinnerDocumentType.setSelection(position);

            editTextDocumentNumber.setText(cursor.getString(5));
            editTextSubmissionDate.setText(cursor.getString(6));
        }
        cursor.close();
    }

    private void saveApplicant() {
        ContentValues values = new ContentValues();
        values.put("full_name", editTextFullName.getText().toString().trim());
        values.put("birth_day", editTextBirthDay.getText().toString().trim());
        values.put("phone", editTextPhone.getText().toString().trim());
        values.put("email", editTextEmail.getText().toString().trim());
        values.put("document_type", spinnerDocumentType.getSelectedItem().toString());
        values.put("document_number", editTextDocumentNumber.getText().toString().trim());
        values.put("submission_date", editTextSubmissionDate.getText().toString().trim());

        if (applicantId == -1) {
            db.insert("applicants", null, values);
            Toast.makeText(this, "Добавлено", Toast.LENGTH_SHORT).show();
        } else {
            db.update("applicants", values, "id = ?", new String[]{String.valueOf(applicantId)});
            Toast.makeText(this, "Обновлено", Toast.LENGTH_SHORT).show();
        }

        finish();
    }

    private void showDatePicker(EditText target) {
        Calendar calendar = Calendar.getInstance();
        DatePickerDialog datePickerDialog = new DatePickerDialog(
                this,
                (view, year, month, dayOfMonth) -> {
                    String date = String.format("%04d-%02d-%02d", year, month + 1, dayOfMonth);
                    target.setText(date);
                },
                calendar.get(Calendar.YEAR),
                calendar.get(Calendar.MONTH),
                calendar.get(Calendar.DAY_OF_MONTH)
        );
        datePickerDialog.show();
    }
}
