   M S B A M L   `   `   `  ����K  GTicketManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:TicketManagement.View.LocationTicketManagement.View.Location  5      7 clr-namespace:RFIDApp.ComponentsRFIDApp.Components  5      1    +TicketManagement.View.Location.LocationView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:TicketManagement.View.Location     5      6	component clr-namespace:RFIDApp.Components     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��3��6   $��10��6D   $��2��6P   $��	10,0,10,0q�6d    � IsIndeterminate .    PrimaryColor#��[  6�   y�6#   �� *	IsLoading+6�   �� 5   
   $��0��6   $��2��6!   $��Địa điểm��65     SecondaryColor#��[ 6E   $
��Bold"�6r   $��30(�6�   $��
15,15,15,0q�6�   � 5   
   $��1��6   $��0��6    � ColumnDefinitions 5      �� 5      �� 5      $��9* �6#     5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��0��6   
�������$��1q�6:    �� Padding$ 5q�6N   $��10,10,10,10q�6Z    �� CornerRadius$ 5,5,0,0��6o   $��2��6�   ._�5       ]� .+�5!      �� $
��Left=�6    $��Danh sách địa điểm��6;   $��20(�6U   $��Center=�6c   $
��Bold"�6~   #��[  6�   �� 5"       �� Orientation$ 
Horizontal=�6!   $��20,0,0,0q�6:   .+�5#      ��   PrimaryButton#��[ 6!   $��Thêm mới��6H   ��6[   �� *
AddCommand+5$      &    RFIDApp.Components.CustomTextBox  5%      $	��200��6.   $��Right=�6:      PlaceHolder$  Tìm kiếm địa điểm��6V      Text 6v   �� *SearchLocation+ J� InputBindings 5&      �� 5'      
	 �� Key$	 Return��6)   
 �� Command
 66   �� *SearchCommand+5)      5*      5+      �� 5-      $��1��6   
�������$��1,0,1,1q�6:   $��	10,0,10,0q�6T   $ 0,0,5,5��6g   $��2��6~   ._�5.      �� $��2q�6   $��0q�6'   ��6;   �� *ListLocation+ �� View 5/      �� 50      .P�51      ��  �� Width$ 30��6-   $��#��68    �� CellTemplate 52   "   �� 53   &   .R�54   *   �� $��16(�6I   ��64   �� *STT+6I   55   '   57      �� 58      $	 200��6-   $��Tên địa điểm��69    59   "   �� 5:   &   .R�5;   *   �� $��16(�6R   ��64   �� *Locationname+6R   5<   '   5>      �� 5?      $	 150��6-   $��Trạng thái��69    5@   "   �� 5A   &   .R�5B   *   �� $��16(�6L   ��64   �� *Status+��5C   .   �� 5D   2    �� 
TargetType" ��68    �� Triggers 5E   6   �� 5F   :    �� Binding 6F   �� *Status+ �� Value Đang hoat động6a   .a�5G   >   ��  �� Property � �� �� Value
 � � �6[   5H   ;   �� 5I   :    6F   �� *Status+ Không hoạt động6a   .a�5J   >   ��  � ��
 �  ��6[   5K   ;   5M   3   5O   +   5P   '   5R      �� 5S      $	 200��6-   $��Hành động��69    5T   "   �� 5U   &   .R�5V   *   �� $ 
Horizontal=�65   .+�5W   .   ��   
EditButton#��[ 65   $��10,0,0,0q�6Y   ��6k   ��  �� RelativeSource ��  �� AncestorType$ ListViewL� �� Path$ DataContext.EditCommand���6�   �� �� 5X   .   #��[ 65   $��Delete��6\   $��10,0,0,0q�6m   ��6   ��  �� $ ListViewL�$ DataContext.DeleteCommand���6�   �� 5Y   +   5Z   '   5\      5]      5_      5`      5b      &    RFIDApp.Components.PaginationBar  5c   
   $��2��6"   $��10q�6/    � DataContext 6;   �� *
Pagination+5d      5e      