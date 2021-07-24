import java.io.File;
import java.io.FileNotFoundException;
import java.util.*;

public class InvertedIndex {
    HashMap<String, String> allStuffs = new HashMap<>();
    HashMap<String, ArrayList<String>> tokenizedWords = new HashMap<>();

    public void tokenizeFiles (){
        FileReader fileReader = new FileReader();
        allStuffs = fileReader.readingFiles();



        for(Map.Entry<String, String> entry : allStuffs.entrySet()) {
            String value = entry.getValue();
            String[] allWords = value.split(" ");
            for (String allWord : allWords) {
                if (tokenizedWords.containsKey(allWord)) {
                    if (!tokenizedWords.get(allWord).contains(entry.getKey())) {
                        tokenizedWords.get(allWord).add(entry.getKey());
                    }
                } else {
                    ArrayList<String> docks = new ArrayList<>();
                    docks.add(entry.getKey());
                    tokenizedWords.put(allWord.toLowerCase(), docks);
                }
            }
        }

        removingStopWords();



        System.out.println(tokenizedWords);

    }

    public void removingStopWords (){
        try {
            File file = new File("StopWords");
            Scanner sc = null;
            sc = new Scanner(file);
            sc.useDelimiter("\\Z");

            String[] mustRemove = sc.next().split("-");
            for (String s : mustRemove) {
                tokenizedWords.remove(s);
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }

    public static void main(String[] args) {
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.tokenizeFiles();
    }
}
