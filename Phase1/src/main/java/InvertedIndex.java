import java.util.HashMap;
import java.util.Map;
import java.util.TreeSet;

public class InvertedIndex {
    HashMap<String, String> allDocuments = new HashMap<>();
    HashMap<String, TreeSet<String>> tokenizedWords = new HashMap<>();

    public void tokenizeFiles (String folderName){
        FileReader fileReader = new FileReader();
        allDocuments = fileReader.readingFiles(folderName);


        for (Map.Entry<String, String> entry : allDocuments.entrySet()) {
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
            result = andWordWithResult(string, result);
        }
        for (String string : userInput.getOrInputs()) {
            result = addWordToResult(string, result);
        }
        for (String string : userInput.getRemoveInputs()) {
            result = removeWordFromResult(string, result);
        }
        if (result != null)
            return result;
        else
            return new TreeSet<>();
    }

    private TreeSet<String> removeWordFromResult(String word, TreeSet<String> result) {
        TreeSet<String> wordList = getDocsContainWord(word);
        if (result == null) {
            return new TreeSet<>();
        }
        result.removeAll(wordList);
        return result;
    }

    private TreeSet<String> addWordToResult(String word, TreeSet<String> result) {
        TreeSet<String> wordList = getDocsContainWord(word);
        if (result == null) {
            return (TreeSet<String>) wordList.clone();
        }
        result.addAll(wordList);
        return result;
    }

    private TreeSet<String> andWordWithResult(String word, TreeSet<String> result) {
        TreeSet<String> wordList = getDocsContainWord(word);
        if (result == null) {
            return (TreeSet<String>) wordList.clone();
        }
        result.retainAll(wordList);
        return result;
    }

    private TreeSet<String> getDocsContainWord(String word) {
        if (tokenizedWords.containsKey(word))
            return tokenizedWords.get(word);
        else
            return new TreeSet<>();
    }
}
