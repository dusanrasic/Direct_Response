   M S B A M L   `   `   `  ����J  FDirect Response, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Direct_ResponseDirect_Response  5   	   $    Direct_Response.RegisterWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Direct_Response     5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	     � Title$  RegisterWindow��5   	   $	��470��6    $	��360��6-    � WindowStyle$
 None=�5	   	    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�66      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $	��100 �6   �� 5      $��* �6   .+�5   
   �� -   5        $��windowBorderDrag��5      $��0��6)   
������ ._�5      � .+�5      (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ PrimaryDark=�6+   ���#37474F.��5      �� $��Right=�6!   .+�5      �� -   5        $��btnRegisterClose��5   !   $��30��69   $��30��6D   $��0q�6P   ���#263238
������ $��20q�6�   $��0q�6�   .��5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ WindowClose=�66   5      5      5       5!      5"      � 5#   
   $��1��6   .+�5$        $ 
PrimaryMid=�6'   ���#37474F$	��100��6N   .��5%      �� $.��(pack://application:,,,/Fonts/#CODE Light)�6   $��Light"�6R   $��REGISTER��6e   $��35(�6u   $��Center=�6�   $��Center=�6�   $��0 30 0 0q�6�   ���#CFD8DC6�   5&      5'      � 5(   
   $��2��6   .+�5)        $ 
PrimaryMid=�6'   ���#37474F$	��340��6N   .��5*      �� 	 �� Orientation$	 Vertical=�6   $��10q�64   .+�5+      �� $	 
Horizontal=�6!   $��10q�6:   .+�5,        $ Account=�62   $��Bottom=�6�   $	��0 4q�6�   ��6A   �� 
 �� ElementName$
 
tbFullName�� �� Path$ BorderBrush�6�   �� 5-      -   5         ��Name$ 
tbFullName��5-   "   $	��290��66   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 	Full_name��6B   $��2 0 0 0q�6m     5.      -   5          Name$ tbFullNameFailIcon��5.   2   $ Information=�6N   $��Bottom=�6�   $	��0 4q�6�   $y�	Collapsed=�6�    � ToolTip$ Alredy exists.��6�   ��6a   �� $
 
tbFullName��$ BorderBrush�6�   5/      �� 50      $	 
Horizontal=�6!   $��10q�6:   .+�51        $ Email=�62   $��Bottom=�6|   $	��0 4q�6�   ��6?   �� $
 tbEmail��$ BorderBrush�6�   �� 52      -   5        $ tbEmail��52   "   $	��290��63   $
 Mail��6?   $��2 0 0 0q�6e     53      -   5        $ tbEmailFailIcon��53   2   $ Information=�6K   $��Bottom=�6�   $	��0 4q�6�   $y�	Collapsed=�6�   $ Invalid e-mail.��6�   ��6^   �� $
 tbEmail��$ BorderBrush�6�   54      �� 55      $	 
Horizontal=�6!   $��10q�6:   .+�56        $ Account=�62   $��Bottom=�6�   $	��0 4q�6�   ��6A   �� $
 
tbUserName��$ BorderBrush�6�   �� 57      -   5        $ 
tbUserName��57   "   $	��290��66   $ 	User_name��6B   $��2 0 0 0q�6m     58      -   5        $ tbUserNameFailIcon��58   2   $ Information=�6N   $��Bottom=�6�   $	��0 4q�6�   $y�	Collapsed=�6�   $ Alredy exists.��6�   ��6a   �� $
 
tbUserName��$ BorderBrush�6�   59      �� 5:      $	 
Horizontal=�6!   $��10q�6:   .+�5;        $	 Key=�62   $��Bottom=�6}   $	��0 4q�6�   ��6=   �� $
 
tbPassword��$ BorderBrush�6�   G� 5<      -	   5         G�Name$ 
tbPassword��5<   &   $	��290��6:   $ Password��6F   $��2 0 0 0q�6p     5=      -
   5        $ tbPasswordFailIcon��5=   2   $ Information=�6N   $��Bottom=�6�   $	��0 4q�6�   $y�	Collapsed=�6�   $ Password's don't match.��6�   ��6a   �� $
 
tbPassword��$ BorderBrush�6�   5>      �� 5?      $	 
Horizontal=�6!   $��10q�6:   .+�5@        $	 Key=�62   $��Bottom=�6   $	��0 4q�6�   ��6=   �� $
 tbRePassword��$ BorderBrush�6�   G� 5A      -   5        $ tbRePassword��5A   &   $	��290��6<   $ Re_Password��6H   $��2 0 0 0q�6u     5B      -   5        $ tbRePasswordFailIcon��5B   2   $ Information=�6P   $��Bottom=�6�   $	��0 4q�6�   $y�	Collapsed=�6�   $ Password's don't match.��6�   ��6c   �� $
 tbRePassword��$ BorderBrush�6�   5C      �� 5D      $	 
Horizontal=�6!   $��30q�6:   .+�5E      �� -   5         ��Name$ btnRegister��5E   !   $��REGISTER��66   $	��150��6I   $��Center=�6U   $��Center=�6r   $��	60 0 80 0q�6�   ���#CFD8DC���#263238���#CFD8DC6�   5F      5G      5H      5I      5J      5K      