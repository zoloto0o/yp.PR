package com.example.ais_abiturient_android;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteException;

import java.io.*;

public class DatabaseHelper {
    private static String DB_NAME = "AIS_Abiturient.db";
    private static String DB_PATH = "";
    private final Context context;

    public DatabaseHelper(Context context) {
        this.context = context;
        DB_PATH = context.getDatabasePath(DB_NAME).getPath();
    }

    public void copyDatabaseIfNeeded() {
        File dbFile = new File(DB_PATH);
        if (!dbFile.exists()) {
            try {
                copyDatabase();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    private void copyDatabase() throws IOException {
        InputStream input = context.getAssets().open(DB_NAME);
        File outFile = new File(DB_PATH);

        outFile.getParentFile().mkdirs(); // Создаёт папку, если нужно
        OutputStream output = new FileOutputStream(outFile);

        byte[] buffer = new byte[1024];
        int length;

        while ((length = input.read(buffer)) > 0) {
            output.write(buffer, 0, length);
        }

        output.flush();
        output.close();
        input.close();
    }

    public SQLiteDatabase getReadableDatabase() throws SQLiteException {
        return SQLiteDatabase.openDatabase(DB_PATH, null, SQLiteDatabase.OPEN_READWRITE);
    }
}
