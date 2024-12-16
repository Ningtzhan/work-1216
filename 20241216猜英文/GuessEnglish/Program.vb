Imports System.IO

Module Program
    Sub Main(args As String())
        ' 讀取 CSV 檔案並將單字存入字典
        Dim words As New Dictionary(Of String, String)
        Dim filePath As String = "words.csv"
        ' 確保檔案存在
        If File.Exists(filePath) Then
            Dim lines() As String = File.ReadAllLines(filePath)
            For Each line In lines
                Dim parts() As String = line.Split(",")
                If parts.Length = 2 Then
                    ' 在添加之前檢查鍵是否已存在
                    If Not words.ContainsKey(parts(0)) Then
                        words.Add(parts(0), parts(1))
                    End If
                End If
            Next
        Else
            Console.WriteLine("檔案未找到！")
            Return
        End If
        ' 進入連續猜題的循環
        Dim random As New Random()
        While True
            ' 隨機選擇一個中文單字
            Dim chineseWord As String = words.Keys(random.Next(words.Count))
            Dim correctEnglishWord As String = words(chineseWord)
            ' 顯示題目
            Console.WriteLine("請猜出這個中文單字對應的英文單字：")
            Console.WriteLine("中文單字是：" & chineseWord)
            ' 生成選項：正確答案 + 隨機的錯誤答案
            Dim options As New List(Of String)
            options.Add(correctEnglishWord)
            ' 確保只有一個錯誤選項
            Dim wrongOption As String
            Do
                wrongOption = words.Values(random.Next(words.Count))
            Loop While wrongOption = correctEnglishWord
            options.Add(wrongOption)
            ' 隨機打亂選項
            options = options.OrderBy(Function(x) random.Next()).ToList()
            ' 顯示選項
            For i As Integer = 0 To options.Count - 1
                Console.WriteLine($"{i + 1}. {options(i)}")
            Next
            ' 玩家輸入選擇
            Dim choice As Integer = 0
            Dim isValid As Boolean = False
            While Not isValid
                Console.Write("請選擇 1 或 2，輸入 0 退出遊戲：")
                isValid = Integer.TryParse(Console.ReadLine(), choice)
                If isValid AndAlso (choice = 1 OrElse choice = 2 OrElse choice = 0) Then
                    ' 如果選擇 0，結束遊戲
                    If choice = 0 Then
                        Console.WriteLine("感謝遊玩！")
                        Return
                    End If
                    ' 如果選擇正確，顯示恭喜訊息
                    If options(choice - 1) = correctEnglishWord Then
                        Console.WriteLine("恭喜你猜對了！")
                    Else
                        Console.WriteLine("很遺憾，答錯了！")
                    End If
                Else
                    isValid = False
                    Console.WriteLine("請輸入有效的選項 (1 或 2，或 0 退出)。")
                End If
            End While
            ' 詢問是否繼續猜題
            Console.WriteLine("是否繼續猜題？(1/2)")
            Dim continueGame As String = Console.ReadLine().ToUpper()
            If continueGame <> "1" Then
                Console.WriteLine("感謝遊玩！")
                Exit While
            End If
        End While
    End Sub
End Module
