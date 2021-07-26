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


        for (Map.Entry<String, String> entry : allStuffs.entrySet()) {
            String value = entry.getValue();
            TreeSet<String> rawAllWords = tokenize(value);
            TreeSet<String> allWords = StringUtils.processRawTokens(rawAllWords);
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
        UserInput userInput = new UserInput(input);
        TreeSet<String> result = null;
        for (String string : userInput.getAndInputs()) {
            result = andWithWord(string, result);
        }
        for (String string : userInput.getOrInputs()) {
            result = orWithWord(string, result);
        }
        for (String string : userInput.getRemoveInputs()) {
            result = removeWord(string, result);
        }
        if (result != null)
            return result;
        else
            return new TreeSet<>();
    }

    private TreeSet<String> removeWord(String word, TreeSet<String> list) {
        TreeSet<String> wordList = getDocSet(word);
        if (list == null) {
            return new TreeSet<>();
        }
        list.removeAll(wordList);
        return list;
    }

    private TreeSet<String> orWithWord(String word, TreeSet<String> list) {
        TreeSet<String> wordList = getDocSet(word);
        if (list == null) {
            return (TreeSet<String>) wordList.clone();
        }
        list.addAll(wordList);
        return list;
    }

    private TreeSet<String> andWithWord(String word, TreeSet<String> list) {
        TreeSet<String> wordList = getDocSet(word);
        if (list == null) {
            return (TreeSet<String>) wordList.clone();
        }
        list.retainAll(wordList);
        return list;
    }

    private TreeSet<String> getDocSet(String word) {
        if (tokenizedWords.containsKey(word))
            return tokenizedWords.get(word);
        else
            return new TreeSet<>();
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
