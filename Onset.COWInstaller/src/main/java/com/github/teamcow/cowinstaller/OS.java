package com.github.teamcow.cowinstaller;

import lombok.AllArgsConstructor;
import org.apache.commons.lang3.SystemUtils;

@AllArgsConstructor
public enum OS {

    WINDOWS("win"), LINUX("linux");

    private String name;

    static OS getOS(String value) {
        if (value == null || value.trim().isEmpty()) {
            return SystemUtils.IS_OS_WINDOWS ? WINDOWS : (SystemUtils.IS_OS_LINUX ? LINUX : null);
        }
        for (OS os : OS.values()) {
            if (os.name.equalsIgnoreCase(value)) {
                return os;
            }
        }
        return null;
    }
}
