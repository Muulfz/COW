package com.github.teamcow.cowinstaller;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonArray;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;

import java.io.File;
import java.io.IOException;
import java.net.URISyntaxException;
import java.net.URL;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Scanner;

@AllArgsConstructor(access = AccessLevel.PRIVATE)
public class CowInstaller {

    static final Gson GSON = new Gson();

    public static void main(String[] args) throws URISyntaxException, IOException {
        System.out.println("________/\\\\\\\\\\\\\\\\\\_______/\\\\\\\\\\_______/\\\\\\______________/\\\\\\_        \n" +
                " _____/\\\\\\////////______/\\\\\\///\\\\\\____\\/\\\\\\_____________\\/\\\\\\_       \n" +
                "  ___/\\\\\\/_____________/\\\\\\/__\\///\\\\\\__\\/\\\\\\_____________\\/\\\\\\_      \n" +
                "   __/\\\\\\______________/\\\\\\______\\//\\\\\\_\\//\\\\\\____/\\\\\\____/\\\\\\__     \n" +
                "    _\\/\\\\\\_____________\\/\\\\\\_______\\/\\\\\\__\\//\\\\\\__/\\\\\\\\\\__/\\\\\\___    \n" +
                "     _\\//\\\\\\____________\\//\\\\\\______/\\\\\\____\\//\\\\\\/\\\\\\/\\\\\\/\\\\\\____   \n" +
                "      __\\///\\\\\\___________\\///\\\\\\__/\\\\\\_______\\//\\\\\\\\\\\\//\\\\\\\\\\_____  \n" +
                "       ____\\////\\\\\\\\\\\\\\\\\\____\\///\\\\\\\\\\/_________\\//\\\\\\__\\//\\\\\\______ \n" +
                "        _______\\/////////_______\\/////____________\\///____\\///_______");
        System.out.println();
        System.out.println("(c) DasDarki 2020. https://github.com/DasDarki/COW");
        System.out.println("_____________________________________________________________________");
        System.out.println();
        System.out.println("[COW: Installer] Starting installer...");
        Scanner scanner = new Scanner(System.in);
        System.out.println("[COW: Installer] Searching for OS...");
        OS os = OS.getOS(null);
        if (os == null) {
            System.out.println("[COW: Installer] ERROR: Current OS is not supported!");
            waitForExit(scanner);
            return;
        }
        System.out.println("[COW: Installer] Select a path (leave empty for current):");
        String dir = scanner.nextLine();
        if (dir == null || dir.trim().isEmpty()) {
            dir = new File(CowInstaller.class.getProtectionDomain().getCodeSource().getLocation()
                    .toURI()).getParentFile().getPath();
        }
        System.out.println("[COW: Installer] Path selected: " + dir);
        if (!isThisDirServer(dir)) {
            System.out.println("[COW: Installer] ERROR: Could not found a server at the select path!");
            waitForExit(scanner);
            return;
        }
        System.out.println("[COW: Installer] Starting... Load UPDATE data...");
        UpdateData data = UpdateData.download();
        if (data == null) {
            System.out.println("[COW: Installer] ERROR: Could not load UPDATE data!");
            waitForExit(scanner);
            return;
        }

        System.out.println("[COW: Installer] Downloading Files...");
        Files.copy(new URL(os == OS.WINDOWS ? data.getWinUrl() : data.getLinUrl()).openStream(), Paths.get(dir + "/cow.zip"));

        System.out.println("[COW: Installer] Install Files...");
        Zip.extract(dir);

        System.out.println("[COW: Installer] Cleaning up...");
        try {
            new File(dir + "/cow.zip").delete();
        } catch (Exception ex) {
            //Ignore
        }

        System.out.println("[COW: Installer] Adjust Server...");
        JsonParser parser = new JsonParser();
        JsonObject config = parser.parse(new String(Files.readAllBytes(Paths.get(dir + "/server_config.json")))).getAsJsonObject();
        JsonArray plugins = config.getAsJsonArray("plugins");
        if (!contains(plugins, "COW.Runtime"))
            plugins.add("COW.Runtime");
        JsonArray packages = config.getAsJsonArray("packages");
        if (!contains(packages, "cow_lua"))
            packages.add("cow_lua");
        String json = new GsonBuilder().setPrettyPrinting().create().toJson(config);
        Files.write(Paths.get(dir + "/server_config.json"), json.getBytes());
        System.out.println("[COW: Installer] Finished! COW successfully installed!");
        waitForExit(scanner);
    }

    private static boolean contains(JsonArray array, String item) {
        for (int i = 0; i < array.size(); i++) {
            if (array.get(i).getAsString().equals(item)) {
                return true;
            }
        }

        return false;
    }

    private static void waitForExit(Scanner scanner) {
        System.out.println();
        System.out.println("Press enter to exit...");
        scanner.nextLine();
    }

    private static boolean isThisDirServer(String path) {
        return new File(path + "/server_config.json").exists();
    }
}
