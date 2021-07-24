import java.util.HashMap;
import java.util.Map;
import java.util.TreeSet;
import java.io.File;
import java.io.FileNotFoundException;
import java.util.*;

public class InvertedIndex {
    HashMap<String, String> allStuffs = new HashMap<>();
    HashMap<String, TreeSet<String>> tokenizedWords = new HashMap<>();

    public void tokenizeFiles (){
        FileReader fileReader = new FileReader();
        allStuffs = fileReader.readingFiles();



        for(Map.Entry<String, String> entry : allStuffs.entrySet()) {
            String value = entry.getValue();
            TreeSet<String> rawAllWords = tokenize(value);
            TreeSet<String> allWords = stem(rawAllWords);
            for (String allWord : allWords) {
                if (tokenizedWords.containsKey(allWord)) {
                    tokenizedWords.get(allWord).add(entry.getKey());
                } else {
                    TreeSet<String> docks = new TreeSet<>();
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

    private TreeSet<String> stem(TreeSet<String> tokens) {
        TreeSet<String> stemTokens = new TreeSet<>();
        for(String token:tokens){
            Stemmer stemmer = new Stemmer();
            stemmer.add(token.toCharArray(),token.length());
            stemmer.stem();
            String stemToken = stemmer.toString();
            stemTokens.add(stemToken);
        }
        return stemTokens;
    }

    private TreeSet<String> tokenize(String string) {
        TreeSet<String> tokens = new TreeSet<>();
        for(int i =0 ;i <string.length();i++){
            if(!Character.isAlphabetic(string.charAt(i)))
                continue;
            StringBuilder token = new StringBuilder();
            while(Character.isAlphabetic(string.charAt(i))){
                token.append(string.charAt(i));
            }
            tokens.add(token.toString());
        }
        return tokens;
    }

    public static void main(String[] args) {
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.tokenizeFiles();
    }
}
