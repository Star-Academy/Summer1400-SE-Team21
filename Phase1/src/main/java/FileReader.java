import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashMap;
import java.util.Objects;
import java.util.Scanner;

public class FileReader {
    HashMap<String, String> fileNameToItsStuffs = new HashMap<>();

    public HashMap<String, String> readingFiles (String foldrName){
        File folder = new File(foldrName);
        listFilesForFolder(folder);

        return fileNameToItsStuffs;
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
                    if (sc.hasNext()){
                        fileNameToItsStuffs.put(fileEntry.getName(), sc.next().replaceAll("[\\t\\n\\r]+"," "));
                    }
                } catch (FileNotFoundException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
