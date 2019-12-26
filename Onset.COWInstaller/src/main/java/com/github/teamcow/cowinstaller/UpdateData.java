package com.github.teamcow.cowinstaller;

import lombok.AllArgsConstructor;
import lombok.Getter;

@AllArgsConstructor
class UpdateData {

    private static final String UPDATE_URL = "https://raw.githubusercontent.com/DasDarki/COW/master/UPDATE.json?token=AE6IDCYTZYWVC6NBFRIIHFK6BUVXM";
    @Getter private String url;

    static UpdateData download() {
        try {
            return null;
        } catch (Exception ex) {
            ex.printStackTrace();
            return null;
        }
    }
}
