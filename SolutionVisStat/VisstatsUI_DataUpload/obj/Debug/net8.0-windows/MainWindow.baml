   M S B A M L   `   `   `  ����P  LVisstatsUI_DataUpload, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:VisstatsUI_DataUploadVisstatsUI_DataUpload  5   	   &     VisstatsUI_DataUpload.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:VisstatsUI_DataUpload     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��3* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5        $��0��5      $��0��6   $��
VisSoorten��6.   $��2q�5      6   �� 5   
   -   5        $��0��5      $��1��6   $��Upload Vissoorten��6.   $��5q�6J   5      �� 5   
   -   5        $��VisHavensFileListBox��5   
   $��1��5      $��0��6   $��2��6/   $��2q�6C   ${�False��5   &   �� 5    
   -   5        $��2��5       $��0��6   $��Havens��6.   $��2q�5!      6   �� 5#   
   -   5        $��2��5#      $��1��6   $��Upload Havens��6.   $��2q�5$      6   �� 5(   
   -   5        $��VissoortenFileListBox��5)      $��6��5(      $��0��6   $��2��6/   $��2q�6C   ${�False��5)   /   �� 5+   
   -   5        $��4��5+      $��0��6   $��upload��6.   $��2q�5,      6   �� 5.   
   -   5        $��4��5.      $��1��6   $��Upload Havens��6.   $��2q�5/      6   �� 51   
   -	   5        $��StatistiekenFileListBox��52      $��5��51      $��0��6   $��2��6/   $��2q�6C   ${�False��52   1   53      54      