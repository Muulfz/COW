package com.github.teamcow.cowinstaller;

import com.google.gson.Gson;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import org.apache.commons.cli.CommandLine;
import org.apache.commons.cli.CommandLineParser;
import org.apache.commons.cli.DefaultParser;
import org.apache.commons.cli.Option;
import org.apache.commons.cli.Options;
import org.apache.commons.cli.ParseException;

import java.io.File;
import java.net.URISyntaxException;

@AllArgsConstructor(access = AccessLevel.PRIVATE)
public class CowInstaller {

    private static final Gson GSON = new Gson();

    public static void main(String[] args) throws ParseException, URISyntaxException {
        CommandLineParser parser = new DefaultParser();
        CommandLine cmd = parser.parse(buildOptions(), args);
        OS os = OS.getOS(cmd.getOptionValue("os"));
        if (os == null) {
            System.out.println("[COW:Installer] ERROR: Current OS is not supported!");
            return;
        }
        String dir = cmd.getOptionValue("dir");
        if (dir == null) {
            dir = new File(CowInstaller.class.getProtectionDomain().getCodeSource().getLocation()
                    .toURI()).getPath();
        }
        System.out.println("[COW:Installer] Starting... Load UPDATE data...");

    }

    private static Options buildOptions() {
        return new Options().addOption(Option.builder("dir").desc("The installation directory in which COW will be installed to")
                .optionalArg(true).hasArg().build())
                .addOption(Option.builder("os").optionalArg(true).hasArg()
                        .desc("The OS for which it will be installed").build());
    }
}
