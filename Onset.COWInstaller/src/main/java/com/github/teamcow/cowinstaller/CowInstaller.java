package com.github.teamcow.cowinstaller;

import com.google.gson.Gson;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;

import java.io.File;
import java.net.URISyntaxException;
import java.util.Scanner;

@AllArgsConstructor(access = AccessLevel.PRIVATE)
public class CowInstaller {

    static final Gson GSON = new Gson();

    public static void main(String[] args) throws URISyntaxException {
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
            return;
        }
        System.out.println("[COW: Installer] Select a path (leave empty for current):");
        String dir = scanner.nextLine();
        if (dir == null || dir.trim().isEmpty()) {
            dir = new File(CowInstaller.class.getProtectionDomain().getCodeSource().getLocation()
                    .toURI()).getPath();
        }
        System.out.println("[COW: Installer] Path selected: " + dir);
        if (!isThisDirServer(dir)) {
            System.out.println("[COW: Installer] ERROR: Could not found a server at the select path!");
            return;
        }
        System.out.println("[COW:Installer] Starting... Load UPDATE data...");
        UpdateData data = UpdateData.download();
        if (data == null) {
            System.out.println("[COW: Installer] ERROR: Could not load UPDATE data!");
            return;
        }
    }

    private static boolean isThisDirServer(String path) {
        return !new File(path + "/package.json").exists();
    }
}
