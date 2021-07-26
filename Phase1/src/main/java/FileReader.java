import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashMap;
import java.util.Objects;
import java.util.Scanner;

public class FileReader {
    HashMap<String, String> allStuffs = new HashMap<>();

    public HashMap<String, String> readingFiles (String foldrName){
        File folder = new File(foldrName);
        listFilesForFolder(folder);

        return allStuffs;
    }

    public void listFilesForFolder(File folder) {
        for (File fileEntry : Objects.requireNonNull(folder.listFiles())) {
            if (fileEntry.isDirectory()) {
                listFilesForFolder(fileEntry);
            } else {
                Scanner sc = null;
                try {
                    sc = new Scanner(fileEntry);
                    sc.useDelimiter("\\Z");
                    try {
                        allStuffs.put(fileEntry.getName(), sc.next().replaceAll("[\\t\\n\\r]+"," "));
                    }catch (Exception e){

                    }
                } catch (FileNotFoundException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
