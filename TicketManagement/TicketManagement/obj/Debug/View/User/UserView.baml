   M S B A M L   `   `   `  ����K  GTicketManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TicketManagement.View.UserTicketManagement.View.User  5      7 clr-namespace:RFIDApp.ComponentsRFIDApp.Components  5      )    #TicketManagement.View.User.UserView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:TicketManagement.View.User     5      6	component clr-namespace:RFIDApp.Components     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��3��6   $��10��6D   $��2��6P   $��	10,0,10,0q�6d    � IsIndeterminate .    PrimaryColor#��[  6�   y�6#   �� *	IsLoading+6�   �� 5   
   $��0��6   $��2��6!   $��Người dùng��65     SecondaryColor#��[ 6G   $
��Bold"�6t   $��30(�6�   $��
15,15,15,0q�6�   � 5   
   $��1��6   $��0��6    � ColumnDefinitions 5      �� 5      �� 5      $��9* �6#     5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��0��6   
�������$��1q�6:    �� Padding$ 5q�6N   $��
10,10,10,0q�6Z    �� CornerRadius$ 5,5,0,0��6n   $��2��6�   ._�5       ]� .+�5!      �� $
��Left=�6    $��Danh sách người dùng��6;   $��20(�6W   $��Center=�6e   $��15,0,0,0q�6�   $
��Bold"�6�   #��[  6�   �� 5"       �� Orientation$ 
Horizontal=�6!   $��20,0,0,0q�6:   .+�5#      ��   PrimaryButton#��[ 6!   $��Thêm mới��6H   ��6[   �� *AddUserCommand+5$      &    RFIDApp.Components.CustomTextBox  5%      $	��200��6.   $��Right=�6:      PlaceHolder$  Tìm kiếm người dùng��6V      Text 6x   �� *
SearchUser+ J� InputBindings 5&      �� 5'      
	 �� Key$	 Return��6)   
 �� Command
 66   �� *SearchCommand+5)      5*      5+      �� 5-      $��1��6   
�������$��1,0,1,1q�6:   $��	10,0,10,0q�6T   $ 0,0,5,5��6g   $��2��6~   ._�5.      �� $��2q�6   $��0q�6'   ��6;   �� *
ListUser+ �� View 5/      �� 50      .P�51      ��  �� Width$ 30��6-   $��#��68    �� CellTemplate 52   "   �� 53   &   .R�54   *   �� $��16(�6I   ��64   �� *STT+6I   55   '   57      �� 58      $	 180��6-   $��Tên tài khoản��69    59   "   �� 5:   &   .R�5;   *   �� $��16(�6N   ��64   �� *
UserName+6N   5<   '   5>      �� 5?      $	 200��6-   $��Tên người dùng��69    5@   "   �� 5A   &   .R�5B   *   �� $��16(�6N   ��64   �� *
FullName+6N   5C   '   5E      �� 5F      $	 100��6-   $��Vai trò��69    5G   "   �� 5H   &   .R�5I   *   �� $��16(�6N   ��64   �� *
RoleName+6N   5K   '   5M      �� 5N      $	 200��6-   $��Địa điểm��69    5O   "   �� 5P   &   .R�5Q   *   �� $��16(�6R   ��64   �� *LocationName+6R   5R   '   5T      �� 5U      $	 200��6-   $��Hành động��69    5V   "   �� 5W   &   .R�5X   *   �� $ 
Horizontal=�65   .+�5Y   .   ��   
EditButton#��[ 65   $��10,0,0,0q�6Y   ��6k   ��  �� RelativeSource ��  �� AncestorType$ ListViewL� �� Path$ DataContext.EditCommand���6�   �� �� 5Z   .   #��[ 65   $��Delete��6\   $��10,0,0,0q�6m   ��6   ��  �� $ ListViewL�$ DataContext.DeleteCommand���6�   �� 5[   +   5\   '   5^      5_      5a      5b      5d      &    RFIDApp.Components.PaginationBar  5e   
   $��2��6"   $��10q�6/    � DataContext 6;   �� *
Pagination+5f      5g      