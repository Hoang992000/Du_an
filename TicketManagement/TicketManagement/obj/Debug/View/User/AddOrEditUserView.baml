   M S B A M L   `   `   `  ����K  GTicketManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TicketManagement.View.UserTicketManagement.View.User  5   	   7 clr-namespace:RFIDApp.ComponentsRFIDApp.Components  5   	   2    ,TicketManagement.View.User.AddOrEditUserView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:TicketManagement.View.User     5   	   6	component clr-namespace:RFIDApp.Components     5   	   -   5          � WindowStyle$
  None=�5       � WindowStartupLocation$ CenterScreen=�6-   $	��420��5	   !   $	��420��61   $	��350��6A   $	��350��6P    � Title 6	   �� *Title+.��5      ��  �� CornerRadius$ 7��6    �� Padding$ 30q�6   
������ ._�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��20(�6/   $
��Bold"�6=   $��0��6O   $��	0,10,0,10q�6\   ��6   �� *Title+6\   �� 5      $��1��6   .+�5      �� $��Tên tài khoản��6      TitleControl#��[  60   �� 5      $��Bottom=�6     PrimaryColor#��[ 61   $��1q�6]   $ 7��6q   ._�5      �� 
������ $��0q�67   
������   DefaultTextboxStyle#��[ 6e   ��6�   �� *
UserName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5      5      �� 5      $��2��6   .+�5      �� $��Tên người dùng��6   #��[  61   �� 5      $��Bottom=�6   #��[ 61   $��1q�6]   $ 7��6q   ._�5      �� 
������ $��0q�67   
������ #��[ 6e   ��6�   �� *
FullName+$ TwoWay=�$ PropertyChanged=�5       5!      �� 5"      $��3��6   .+�5#      �� $��Mật khẩu��6   #��[  6+   ,   &RFIDApp.Components.BindablePasswordBox  5$         Password 60   �� *
Password+5%      �� 5&      $��4��6   	 �� Orientation$	 
Horizontal=�6&   .+�5'      �� $��Admin:��6   #��[  6)   �� 5)      $��20,0,0,0q�6   $��Admin��6-     
HiddenText#��[ 6z   #��[ 6�   {�6=   �� *
IsChange+
 d� 	IsChecked
 6\   �� *	IsAdmin+6�   5+      � 5,      $��5��6   $��0,30,0,0q�6     � ColumnDefinitions 5-      �� 5.      $��* �6'   �� 5/      $��* �6'   .+�51      ��   CancelButton#��[ 6   $��0��6?   ��52      ��  �� RelativeSource ��  �� AncestorType" ���53      �� *CancelCommand+�� 54        
SaveButton#��[ 6   $��1��6=   ��55      ��  �� " ���56      �� *SaveCommand+57      58      59      5:      