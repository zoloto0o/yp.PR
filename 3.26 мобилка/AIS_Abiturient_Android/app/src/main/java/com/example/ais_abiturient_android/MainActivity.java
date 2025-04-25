package com.example.ais_abiturient_android;

import android.content.Intent;
import android.os.Bundle;
import androidx.appcompat.app.AppCompatActivity;

public class MainActivity extends AppCompatActivity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // Сразу переходим к экрану логина
        Intent intent = new Intent(this, LoginActivity.class);
        startActivity(intent);
        finish(); // закрываем MainActivity
    }
}
