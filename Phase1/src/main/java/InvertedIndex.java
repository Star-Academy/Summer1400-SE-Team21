import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

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
                    tokenizedWords.put(allWord, docks);
                }
            }
        }

        System.out.println(tokenizedWords);

    }

    public static void main(String[] args) {
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.tokenizeFiles();
    }
}
