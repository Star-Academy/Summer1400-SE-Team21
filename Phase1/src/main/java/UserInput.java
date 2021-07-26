import java.util.TreeSet;

public class UserInput {
    private TreeSet<String> andInputs = new TreeSet<>();
    private TreeSet<String> orInputs = new TreeSet<>();
    private TreeSet<String> removeInputs = new TreeSet<>();

    public UserInput(String input) {
        TreeSet<String> userInputTokens = tokenizeUserInput(input);

        for (String string : userInputTokens) {
            if (string.startsWith("+"))
                orInputs.add(string.substring(1));
            else if (string.startsWith("-"))
                removeInputs.add(string.substring(1));
            else
                andInputs.add(string);
        }
        andInputs = StringUtils.processRawTokens(andInputs);
        orInputs = StringUtils.processRawTokens(orInputs);
        removeInputs = StringUtils.processRawTokens(removeInputs);
    }

    public TreeSet<String> getAndInputs() {
        return andInputs;
    }

    public TreeSet<String> getOrInputs() {
        return orInputs;
    }

    public TreeSet<String> getRemoveInputs() {
        return removeInputs;
    }

    private TreeSet<String> tokenizeUserInput(String input) {
        TreeSet<String> tokens = new TreeSet<>();
        String[] splitInput = input.split("\\s+");
        for (String string : splitInput) {
            tokens.add(string.toLowerCase());
        }
        return tokens;
    }
}
