package com.github.teamcow.cowinstaller;

import lombok.AllArgsConstructor;
import lombok.Getter;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;

@AllArgsConstructor
class UpdateData {

    private static final String UPDATE_URL = "https://gta.legionofsensei.de/onset/COW.json";
    @Getter private String winUrl;
    @Getter private String linUrl;

    static UpdateData download() {
        try {
            return CowInstaller.GSON.fromJson(getUpdateContent(), UpdateData.class);
        } catch (Exception ex) {
            ex.printStackTrace();
            return null;
        }
    }

    private static String getUpdateContent() throws IOException {
        URL url = new URL(UPDATE_URL);
        URLConnection connection = url.openConnection();
        BufferedReader reader = new BufferedReader(new InputStreamReader(connection.getInputStream()));
        StringBuilder result = new StringBuilder();
        String line;
        while ((line = reader.readLine()) != null)
            result.append(line);
        return result.toString();
    }
}
