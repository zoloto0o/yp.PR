package com.example.ais_abiturient_android;

import android.content.ContentValues;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.app.AppCompatDelegate;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import org.apache.poi.ss.usermodel.*;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

import java.io.FileOutputStream;
import java.util.ArrayList;

public class AdminActivity extends AppCompatActivity {

    RecyclerView recyclerView;
    ApplicantAdapter adapter;
    ArrayList<Applicant> applicantList = new ArrayList<>();
    SQLiteDatabase db;
    DatabaseHelper dbHelper;
    Button buttonAdd, buttonEdit, buttonDelete, buttonExport;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_admin);

        recyclerView = findViewById(R.id.recyclerView);
        buttonAdd = findViewById(R.id.buttonAdd);
        buttonEdit = findViewById(R.id.buttonEdit);
        buttonDelete = findViewById(R.id.buttonDelete);
        buttonExport = findViewById(R.id.buttonExport);

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

        buttonEdit.setOnClickListener(v -> {
            Applicant selected = adapter.getSelectedApplicant();
            if (selected == null) {
                Toast.makeText(this, "Выберите запись", Toast.LENGTH_SHORT).show();
                return;
            }
            Intent intent = new Intent(this, ApplicantActivity.class);
            intent.putExtra("id", selected.id);
            startActivity(intent);
        });

        buttonDelete.setOnClickListener(v -> {
            Applicant selected = adapter.getSelectedApplicant();
            if (selected == null) {
                Toast.makeText(this, "Выберите запись", Toast.LENGTH_SHORT).show();
                return;
            }
            db.execSQL("DELETE FROM applicants WHERE id = ?", new Object[]{selected.id});
            loadApplicants();
            Toast.makeText(this, "Удалено", Toast.LENGTH_SHORT).show();
        });

        buttonExport.setOnClickListener(v -> exportToExcel());
    }

    @Override
    protected void onResume() {
        super.onResume();
        loadApplicants();
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

    private void exportToExcel() {
        try {
            Workbook workbook = new XSSFWorkbook();
            Sheet sheet = workbook.createSheet("Applicants");

            Row headerRow = sheet.createRow(0);
            String[] headers = {"ID", "ФИО", "Дата рождения", "Телефон", "Email", "Тип документа", "Номер документа", "Дата подачи"};
            for (int i = 0; i < headers.length; i++) {
                Cell cell = headerRow.createCell(i);
                cell.setCellValue(headers[i]);
            }

            for (int i = 0; i < applicantList.size(); i++) {
                Applicant a = applicantList.get(i);
                Row row = sheet.createRow(i + 1);
                row.createCell(0).setCellValue(a.id);
                row.createCell(1).setCellValue(a.fullName);
                row.createCell(2).setCellValue(a.birthDay);
                row.createCell(3).setCellValue(a.phone);
                row.createCell(4).setCellValue(a.email);
                row.createCell(5).setCellValue(a.documentType);
                row.createCell(6).setCellValue(a.documentNumber);
                row.createCell(7).setCellValue(a.submissionDate);
            }

            ContentValues values = new ContentValues();
            values.put(MediaStore.MediaColumns.DISPLAY_NAME, "abiturients.xlsx");
            values.put(MediaStore.MediaColumns.MIME_TYPE, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            values.put(MediaStore.MediaColumns.RELATIVE_PATH, "Download/");

            Uri uri = getContentResolver().insert(MediaStore.Files.getContentUri("external"), values);

            if (uri != null) {
                try (FileOutputStream out = (FileOutputStream) getContentResolver().openOutputStream(uri)) {
                    workbook.write(out);
                    Toast.makeText(this, "Экспорт завершён в Download/", Toast.LENGTH_LONG).show();
                }
            }

            workbook.close();

        } catch (Exception e) {
            e.printStackTrace();
            Toast.makeText(this, "Ошибка экспорта: " + e.getMessage(), Toast.LENGTH_LONG).show();
        }
    }

    // ======= Меню: Переключение темы =======

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu_theme, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == R.id.action_toggle_theme) {
            int mode = AppCompatDelegate.getDefaultNightMode();
            if (mode == AppCompatDelegate.MODE_NIGHT_YES) {
                AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_NO);
            } else {
                AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_YES);
            }
            recreate(); // перезапуск для применения новой темы
            return true;
        }
        return super.onOptionsItemSelected(item);
    }
}
