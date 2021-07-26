import java.util.Scanner;
import java.util.TreeSet;

public class Main {
    public static void main(String[] args) {
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.tokenizeFiles("Docs");
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
