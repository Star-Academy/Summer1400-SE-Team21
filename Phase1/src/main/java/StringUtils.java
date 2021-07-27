import java.io.File;
import java.io.FileNotFoundException;
import java.util.Arrays;
import java.util.Scanner;
import java.util.TreeSet;

public class StringUtils {
    public static void removingStopWords(TreeSet<String> theTreeSet) {
        try {
            File file = new File("StopWords");
            Scanner sc = new Scanner(file);
            sc.useDelimiter("\\Z");

            String[] mustRemove = sc.next().split("-");
            Arrays.asList(mustRemove).forEach(theTreeSet::remove);
            for (String s : mustRemove) {
                theTreeSet.remove(s);
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }

    public static TreeSet<String> processRawTokens(TreeSet<String> rawTokens) {
        removingStopWords(rawTokens);
        rawTokens = stem(rawTokens);
        return rawTokens;
    }

    public static TreeSet<String> stem(TreeSet<String> tokens) {
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
}
