import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;
import java.util.TreeSet;

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
    }

    public void removingStopWords() {
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
        for (String token : tokens) {
            Stemmer stemmer = new Stemmer();
            stemmer.add(token.toCharArray(), token.length());
            stemmer.stem();
            String stemToken = stemmer.toString();
            stemTokens.add(stemToken);
        }
        return stemTokens;
    }

    private TreeSet<String> tokenize(String string) {
        TreeSet<String> tokens = new TreeSet<>();
        for (int i = 0; i < string.length(); i++) {
            if (!Character.isAlphabetic(string.charAt(i)))
                continue;
            StringBuilder token = new StringBuilder();
            while (i < string.length() && Character.isAlphabetic(string.charAt(i))) {
                token.append(Character.toLowerCase(string.charAt(i)));
                i++;
            }
            tokens.add(token.toString());
        }
        return tokens;
    }

    public TreeSet<String> query(String input) {
        TreeSet<String> andInputs = new TreeSet<>();
        TreeSet<String> orInputs = new TreeSet<>();
        TreeSet<String> removeInputs = new TreeSet<>();
        TreeSet<String> userInputTokens = tokenizeUserInput(input);

        for(String string:userInputTokens){
            if(string.startsWith("+"))
                orInputs.add(string.substring(1));
            else if(string.startsWith("-"))
                removeInputs.add(string.substring(1));
            else
                andInputs.add(string);
        }
        andInputs = stem(andInputs);
        orInputs = stem(orInputs);
        removeInputs = stem(removeInputs);

        TreeSet<String> result = null;
        for(String string:andInputs){
            result = andWithWord(string,result);
        }
        for(String string:orInputs){
            result = orWithWord(string,result);
        }
        for(String string:removeInputs){
            result = removeWord(string,result);
        }
        if(result!=null)
            return result;
        else
            return new TreeSet<>();
    }

    private TreeSet<String> removeWord(String word, TreeSet<String> list) {
        TreeSet<String> wordList = getDocSet(word);
        if(list==null){
            return (TreeSet<String>) wordList.clone();
        }
        list.removeAll(wordList);
        return list;
    }

    private TreeSet<String> orWithWord(String word, TreeSet<String> list) {
        TreeSet<String> wordList = getDocSet(word);
        if(list==null){
            return (TreeSet<String>) wordList.clone();
        }
        list.addAll(wordList);
        return list;
    }

    private TreeSet<String> andWithWord(String word, TreeSet<String> list) {
        TreeSet<String> wordList = getDocSet(word);
        if(list==null){
            return (TreeSet<String>) wordList.clone();
        }
        list.retainAll(wordList);
        return list;
    }

    private TreeSet<String> getDocSet(String word){
        if(tokenizedWords.containsKey(word))
            return tokenizedWords.get(word);
        else
            return new TreeSet<>();
    }

    private TreeSet<String> tokenizeUserInput(String input) {
        TreeSet<String> tokens = new TreeSet<>();
        String[] splitInput = input.split("\\s+");
        for(String string:splitInput){
            tokens.add(string.toLowerCase());
        }
        return tokens;
    }

    public static void main(String[] args) {
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.tokenizeFiles();
        Scanner scanner = new Scanner(System.in);

        while (true){
            String input = scanner.nextLine();
            if(input.equals("exit"))
                break;
            TreeSet<String> containingDocs = invertedIndex.query(input);
            if(containingDocs.isEmpty())
                System.out.println("no doc found");
            for(String docName:containingDocs)
                System.out.println(docName);
        }
    }
}
