package com.github.teamcow.cowinstaller;

import lombok.AllArgsConstructor;
import lombok.Getter;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.io.StringWriter;
import java.io.Writer;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.charset.StandardCharsets;
import java.util.List;
import java.util.Map;

@AllArgsConstructor
class UpdateData {

    private static final String UPDATE_URL = "https://raw.githubusercontent.com/DasDarki/COW/master/UPDATE.json";
    @Getter private String url;

    static UpdateData download() {
        try {
            return CowInstaller.GSON.fromJson(getUpdateContent(), UpdateData.class);
        } catch (Exception ex) {
            ex.printStackTrace();
            return null;
        }
    }

    private static String getUpdateContent() throws IOException {
        String link = UPDATE_URL;
        URL url = new URL(link);
        HttpURLConnection connection = (HttpURLConnection) url.openConnection();
        Map<String, List<String>> list = connection.getHeaderFields();
        for (String header : list.get(null)) {
            if (header.contains(" 302 ") || header.contains(" 301 ")) {
                link = list.get("Location").get(0);
                url = new URL(link);
                connection = (HttpURLConnection) url.openConnection();
                list = connection.getHeaderFields();
            }
        }
        InputStream stream = connection.getInputStream();
        String response = getStringFromStream(stream);
        stream.close();
        connection.disconnect();
        return response;
    }

    private static String getStringFromStream(InputStream stream) throws IOException {
        if (stream != null) {
            Writer crunchifyWriter = new StringWriter();
            char[] crunchifyBuffer = new char[2048];
            try {
                Reader crunchifyReader = new BufferedReader(new InputStreamReader(stream, StandardCharsets.UTF_8));
                int counter;
                while ((counter = crunchifyReader.read(crunchifyBuffer)) != -1) {
                    crunchifyWriter.write(crunchifyBuffer, 0, counter);
                }
            } finally {
                stream.close();
            }
            return crunchifyWriter.toString();
        } else {
            return "No Contents";
        }
    }
}
