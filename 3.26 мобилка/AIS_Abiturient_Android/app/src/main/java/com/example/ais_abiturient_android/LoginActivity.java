package com.example.ais_abiturient_android;

import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.*;
import androidx.appcompat.app.AppCompatActivity;

public class LoginActivity extends AppCompatActivity {

    EditText editTextUsername, editTextPassword;
    Button buttonLogin;
    SQLiteDatabase db;
    DatabaseHelper dbHelper;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        editTextUsername = findViewById(R.id.editTextUsername);
        editTextPassword = findViewById(R.id.editTextPassword);
        buttonLogin = findViewById(R.id.buttonLogin);

        try {
            dbHelper = new DatabaseHelper(this);
            dbHelper.copyDatabaseIfNeeded();
            db = dbHelper.getReadableDatabase();

            // Тест — проверим наличие пользователей в таблице
            Cursor testCursor = db.rawQuery("SELECT COUNT(*) FROM users", null);
            if (testCursor.moveToFirst()) {
                int count = testCursor.getInt(0);
                Toast.makeText(this, "Пользователи в базе: " + count, Toast.LENGTH_LONG).show();
                Log.d("DB_CHECK", "Пользователи в базе: " + count);
            }
            testCursor.close();

        } catch (Exception e) {
            Toast.makeText(this, "Ошибка БД: " + e.getMessage(), Toast.LENGTH_LONG).show();
            Log.e("DB_ERROR", "Ошибка при подключении к базе: ", e);
        }

        buttonLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                loginCheck();
            }
        });
    }

    private void loginCheck() {
        String username = editTextUsername.getText().toString().trim().toLowerCase();
        String password = editTextPassword.getText().toString().trim();

        if (db == null) {
            Toast.makeText(this, "База данных недоступна", Toast.LENGTH_SHORT).show();
            return;
        }

        Cursor cursor = null;
        try {
            String query = "SELECT role FROM users WHERE LOWER(username)=? AND password=?";
            cursor = db.rawQuery(query, new String[]{username, password});

            if (cursor.moveToFirst()) {
                String role = cursor.getString(0);
                Toast.makeText(this, "Успешный вход как " + role, Toast.LENGTH_SHORT).show();

                if (role.equalsIgnoreCase("admin")) {
                    startActivity(new Intent(this, AdminActivity.class));
                } else if (role.equalsIgnoreCase("operator")) {
                    startActivity(new Intent(this, OperatorActivity.class));
                }

            } else {
                Toast.makeText(this, "Неверные данные!", Toast.LENGTH_SHORT).show();
            }

        } catch (Exception e) {
            Toast.makeText(this, "Ошибка при входе: " + e.getMessage(), Toast.LENGTH_LONG).show();
            Log.e("LOGIN_ERROR", "Ошибка при входе: ", e);
        } finally {
            if (cursor != null) cursor.close();
        }
    }
}
