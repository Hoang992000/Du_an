   M S B A M L   `   `   `  ����K  GTicketManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:RFIDApp.ComponentsRFIDApp.Components  5      Y UMaterial.Icons.WPF, Version=2.1.0.0, Culture=neutral, PublicKeyToken=34ed5903a64b4509S<clr-namespace:Material.Icons.WPF;assembly=Material.Icons.WPFMaterial.Icons.WPF 5      &     RFIDApp.Components.PaginationBar   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:RFIDApp.Components     5      Micon<clr-namespace:Material.Icons.WPF;assembly=Material.Icons.WPF     5        � 	Resources  5       �� 
TargetType �� Property �� Value��%z   '��       �� 5   
   " ��6   .�5      ��  � ��$ 16(�6)   �� 5       � ��$ Center=�62   5      5      .��5      �  � DataContext 6   ��  � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5      �� 5      .+�5   
   �� $��1��6    �� Orientation$ 
Horizontal=�6%   .+�5      �� $��Hiển thị:��6   �� 5      -   5         ��Name$ 
cbPageSize��5      $��70��6+   $��5q�66   $��	10,0,10,0q�6B   $��16(�6U    �� SelectedItem 6c   �� *
PageSize+	 �� NotifyOnSourceUpdated	 . 
 �� UpdateSourceTrigger$
 PropertyChanged=��� 5      $��Kết quả��6   5       �� 5!   
   $��0��6   $
��Left=�6%   $ 
Horizontal=�6@   .+�5"      �� $��Từ ��6   �� 5#      ��6   �� *
StartIndex+	 . $
 PropertyChanged=��� 5$      $�� Đến ��6   �� 5%      ��6   �� *
EndIndex+	 . $
 PropertyChanged=��� 5&      $�� Trong tổng số ��6   �� 5'      ��6   �� *
TotalItems+	 . $
 PropertyChanged=�5)      �� 5*   
   $��2��6   $ 
Horizontal=�6%   $��Right=�6>   .+�5+      ��    PrimaryButton#��[  6   $��10,0,0,0q�6a   $��30��6s   $��35��6   ��6<   �� *FirstPageCommand+.��5,      %  Material.Icons.WPF.MaterialIcon     Kind$ StepBackward2=�6$   $��15��69   $��15��6D   5-      �� 5.      #��[  6   $��10,0,0,0q�6e   $��30��6w   $��35��6�   ��6<   �� *PreviousPageCommand+.��5/        $ StepBackward=�6$   $��15��68   $��15��6C   50      �� 51      -   5        $��16(�51   ~   $��Center=�52      $��5q�6:   $��60��6F    �� 	MaxLength$ 4��6Q   $��	10,0,10,0q�6_   ��51      �� *CurrentPage+	 . $
 PropertyChanged=� �� Mode$ TwoWay=�.��54       J� InputBindings 55      �� 56      
 �� Key$ Enter��6G    �� Command 6!   ��  �� Path$ InputCommand�6G   .��57   )   58      �� 59      $��/ ��6   $��16(�6"    �� Padding$ 5q�60   �� 5:      $��Center=�6s   $��	10,0,10,0q�6�   ��6   �� *
TotalPages+	 . $
 PropertyChanged=�6�   �� 5;      #��[  6   $��10,0,0,0q�6a   $��30��6s   $��35��6   ��6<   �� *NextPageCommand+.��5<        $ StepForward=�6$   $��15��67   $��15��6B   5=      �� 5>      #��[  6   $��10,0,0,0q�6`   $��30��6r   $��35��6~   ��6<   �� *LastPageCommand+.��5?        $ StepForward2=�6$   $��15��68   $��15��6C   5@      5A      5B      5C      