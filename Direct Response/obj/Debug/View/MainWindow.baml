   M S B A M L   `   `   `  ����J  FDirect Response, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Direct_ResponseDirect_Response  5   	   E'clr-namespace:Direct_Response.ViewModelDirect_Response.ViewModel  5   	   =#clr-namespace:Direct_Response.ModelDirect_Response.Model  5   	   A%clr-namespace:Direct_Response.UtilityDirect_Response.Utility  5	   	        Direct_Response.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Direct_Response     5   	   6dr'clr-namespace:Direct_Response.ViewModel     5   	   3con#clr-namespace:Direct_Response.Model     5   	   3u%clr-namespace:Direct_Response.Utility     5	   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  Direct Response��5   	   $	��650��6!   $	��850��6.    � WindowStyle$
 None=�6:    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�6g      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � UseLayoutRounding .  � 	Resources 5       �� DataType&    Direct_Response.ViewModel.SentVM	 �� Orientation+   %Direct_Response.ViewModel.DeliveredVM.   (Direct_Response.ViewModel.ConversationVM
 ��Name �� BitmapScalingMode �� EdgeMode �� ImageSource A�Name �� 
TargetType �� Triggers �� Binding �� Value �� Property �� Value ��Name � ContextMenu m� Command  OnReturnAddMessage��%  (��       5      *, +)(�� [    5!      *, +)(�� �    5(      *, +)&	 �    �� 5   
   "  6   .R�5      �� $	 Vertical=�6   $��10q�60   $��Right=�6<   $
��Auto��6X   $	��500��6e   $
��Auto��6t   .+�5      �� 
���   ����#fbfbfb$��5q�6@   $��Right=�6T   .��5      �� $��WrapWithOverflow=�6    ���#00fbfb$��20(�6q   ��6U   �� *
Model.Text+6q   5      5      5       �� 5!   
   "  6   .R�5"      �� $	 Vertical=�6   $��10q�60   $
��Left=�6<   $
��Auto��6W   $	��500��6d   $
��Auto��6s   .+�5#      �� 
���   ����#00fbfb$��5q�6@   $
��Left=�6T   .��5$      �� $��WrapWithOverflow=�6    
�������$��20(�6o   ��6S   �� *
Model.Text+6o   5%      5&      5'      �� 5(   
   "  6   .R�5)      �� $
 Conversation��6   $	 
Horizontal=�6/   $��5 0 0 0q�6H   .+�5*      A� $��50��6   $��50��6%   $��Center=�61   $��Center=�6L   ��5+      �� 5,      ConversationImage��$ NearestNeighbor=�6d   $ Aliased=�6�    6@   �� *Model.Image+6�   5.      A� 5/      $ ConversationImageRing��6   $��52��69   $��50��6D   $��Center=�6P   $��Center=�6k   $��	-50 0 0 0q�6�   $��2��6�   ��50      �� 51      " A�6     52      �� 53   "    6.   �� *
Model.Seen+ 16M   .a�54   &   ��  � ��
 � E��6?   55   #   �� 56   "    6.   �� *
Model.Seen+ 06M   .a�57   &   ��  � ��
 ���� 6?   58   #   5:      5<      �� 5=      $ ConversationFullName��6   $
��Auto��6:   $��20 0 0 0q�6G   $��Center=�6Y   $��15(�6t   ��6�   �� *
Model.Name+ 5>      �� 5?      .@�5@      m� $��Delete��6#    63   �� *Delete+5A      5C      5D      �� 5E   
   6   5F       J� CommandBindings 5G      )  #System.Windows.Input.CommandBinding  5H   
   -   5           Command# [ 5H      6G   .��5J      �  � RowDefinitions 5K   
   �� 5L      $��40 �6   �� 5M      $��3* �6   ��5O   
   �� 5P       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 0,1&�63   .��5Q      �  � Color$ #000000��6    � Offset$	 0.0Q�6/   � 5R      $ #000000��6   $
 0.25Q�6/   � 5S      $ #0d0d0d��6   $
 0.50Q�6/   � 5T      $ #263238��6   $
 0.75Q�6/   � 5U      $ #455A64��6   $ 1Q�6/   5V      .+�5X   
   �� -   5        $��windowBorderDrag��5X      $��0��6)   
������ ._�5Y      � .+�5Z      (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ PrimaryDark=�6+   
������ .��5[      �� $��Right=�6!   .+�5\      �� -   5        $��WindowMinimize��5\   !   $��40��67   $��40��6B   $��0q�6N   $��0 0 80 0q�6w   ���#37474F
������ $��20q�6�   .��5]      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]    Kind$  WindowMinimize=�66   5^      �� 5_      -   5        $��WindowMaximize��5_   !   $��40��67   $��40��6B   $��0q�6N   $��40 -40 40 0q�6w   ���#37474F
������ $��20q�6�   .��5`        $  WindowMaximize=�66   5a      �� 5b      -   5        $��WindowClose��5b   !   $��40��64   $��40��6?   $��0q�6K   $��
80 -40 0 0q�6q   ���#37474F
������ $��20q�6�   .��5c        $  WindowClose=�66   5d      5e      5f      �� 5g      -   5        $
 DirectResponseBorder��5g      $
��Left=�6;   $��Center=�6V   .+�5h      �� $.��(pack://application:,,,/Fonts/#CODE Light)�6    $��Light"�6V   $��17(�6i   $��10 0 0 0q�6w   $��Direct Response��6�   ���#CFD8DC6�   5i      5j      5k      � 5l   
   -   5        ! �Name$! LogInSection��5l      $��1��6%   $y�	Collapsed=�62   
������  5m      �� 5n      $��2* �6    �� 5o      $��2* �6    .+�5q      � -	   5        $! LogoRow��5q      $��0��6$   
������ $��0 80 0 0q�6K   .+�5r      �� $	 
Horizontal=�6   $��Center=�66   $��Center=�6S   $	��250��6n   .+�5s      �� $.��(pack://application:,,,/Fonts/#CODE Light)�6    $	��210(�6V   $��D��6e   ���#fbfbfb$��
UltraLight"�6�   $��
31 0 -31 0q�6�   �� 5t      $.��(pack://application:,,,/Fonts/#CODE Light)�6    $	��210(�6V   $��R��6e   ���#00fbfb$��
UltraLight"�6�   $��	-31 0 0 0q�6�   5u      5v      � 5w      -
   5        $! LogInFormRow��5w      $��1��6)   
������ $��	0 -10 0 0q�6O   .+�5x        $ 
PrimaryMid=�6+   
������ $	��300��6W   .��5y      �� -   5        $
 	LogInForm��5y   !   $	 Vertical=�64   $��Center=�6K   $��Bottom=�6h   " J� Opacity$" 0Q�6�   $
��0 20q�6�   .+�5z      �� $	 
Horizontal=�6%   $��10q�6>   .+�5{        -   5        #  Name$# tbUserNameIcon��5{   6   $  Account=�6N   $��Bottom=�6�   $	��0 4q�6�   ��6]   �� $ �� ElementName$$ 
tbUserName��% �� Path$% BorderBrush�6�   �� 5|      -   5        & ��Name$& 
tbUserName��5|   &   $	��400��6:   )	  #MaterialDesignThemes.Wpf.HintAssist' 	  Hint$' 	User_name��6F   $��2 0 0 0q�6q     5}      -   5        $# tbUserNameFailIcon��5}   6   $  Information=�6R   $��Bottom=�6�   $	��0 4q�6�   $y�	Collapsed=�6�   ( � ToolTip$( Invalid username.��6�   ��6e   �� $$ 
tbUserName��$% BorderBrush�6�   5~      �� 5      $	 
Horizontal=�6%   $��10q�6>   .+�5�        -   5        $# tbPasswordIcon��5�   6   $	  Key=�6N   $��Bottom=�6�   $	��0 4q�6�   ��6Y   �� $$ 
tbPassword��$% BorderBrush�6�   G� 5�      -   5        ) G�Name$) 
tbPassword��5�   *   $	��400��6>   $' Password��6J   $��2 0 0 0q�6t     5�      -   5        $# tbPasswordFailIcon��5�   6   $  Information=�6R   $��Bottom=�6�   $	��0 4q�6�   $y�	Collapsed=�6�   $( Invalid password.��6�   ��6e   �� $$ 
tbPassword��$% BorderBrush�6�   5�      �� 5�      $	 
Horizontal=�6%   $��10q�6>   .+�5�      �� -   5        * ��Name$* chbRememberMe��5�   '   $��Remember me��6>   5�      �� 5�      $	 
Horizontal=�6%   $��30q�6>   .+�5�      �� -   5        + ��Name$+ btnLogin��5�   %   $��LOG_IN��67   $	��150��6H   $��Center=�6T   $��Center=�6q   $��100 0 100 0q�6�   ���#CFD8DC���#263238���#CFD8DC6�   5�      �� 5�      $	 
Horizontal=�6%   $��80 10 10 10q�6>   .+�5�      �� $��Don't have an account?��6(   �� 5�      $��10 0 0 0q�6)   �� 5�      -   5        , ��Name$, HyperLinkRegister��5�   (   .�6c   Create an one.6s   5�      5�      5�      5�      5�      5�      � 5�   
   -   5        $! LandingSection��5�      $��1��6'   $y�Visible=�64   ���#263238- � ColumnDefinitions- 5�      �� 5�      $	��250 �6#   �� 5�      $��2* �6#   .+�5�      � -   5        $
! Menu��5�      $��0��6!   ���#263238$��	0 0 175 0q�6F    5�      �� 5�      $��50 �6$   �� 5�      $��3* �6$   �� 5�      $��50 �6$   .+�5�      �� -   5        $
 menuNavigation��5�      $��Center=�65   $��Right=�6P   $y�Visible=�6l   .+�5�      �� -   5        $+ hideMenu��5�      $��40��6/   $��40��6:   $��0q�6F   $��0 0 15 0q�6R   $y�	Collapsed=�6d   
������ 
������ $��10q�6�   .��5�        $  Close=�62   $��40��6?   $��40��6J   ���#CFD8DC6V   5�      �� 5�      -   5        $+ showMenu��5�      $��40��6/   $��40��6:   $��0q�6F   $��0 0 15 0q�6R   $y�Visible=�6d   
������ 
������ $��10q�6�   .��5�        $
  Menu=�62   $��40��6>   $��40��6I   ���#CFD8DC6U   5�      5�      � 5�      $��1��6    5�      �� 5�      $��* �6(   �� 5�      $��50 �6(   .+�5�      �� -   5        . ��Name$. ListOfConversations��5�      $��0��6�   $��Hidden=�6�   $��Hidden=�6�   $y�Visible=�6  $��0q�6&  $��0q�62  $��Stretch=�6=  $ HighQuality=�6b  $/ �� IsSynchronizedWithCurrentItem$
/ TrueY�6�  ��6;   �� *CurrentUserVM.RootVM.Messages+6�  �� 5�      -   5        $+ btnAddConversation��5�      $��
10 0 10 10q�69   $��1��6M   $��Bottom=�6Z   $��Right=�6u   $��40��6�   $��40��6�    & "MaterialDesignFloatingActionButton#��[ 6�   $( Add Conversation��6�   $y�	Collapsed=�6�   $ HighQuality=�67  .��5�        $
  Plus=�62   $��30��6>   $��30��6I   5�      5�      �� 5�      -   5        $
 settingsPanel��5�      $��2��64   $	��250��6A   $
��Auto��6M   ���#263238$��0 0 0 0q�6p   .+�5�      �� -   5        $+ btnSettings��5�      $��
-400 0 0 0q�62   $��Settings��6F   $	��100��6Y   6e   �� 5�      -   5        $+ hideSettings��5�      $��40��63   $��40��6>   $��0q�6J   $��	170 0 0 0q�6V   $y�	Collapsed=�6i   
������ 
������ $��10q�6�   .��5�        $  Close=�62   $��40��6?   $��40��6J   ���#CFD8DC6V   5�      � 5�      $	��250��6   $	��580��6'   $��0 10 0 0q�64    5�      �� 5�      $��40 �6,   �� 5�      $��2* �6,   �� 5�      $��40 �6,   �� 5�      $��3* �6,   �� 5�      $��40 �6,   �� 5�      $��* �6,   .+�5�      �� $��0��6$   $.��(pack://application:,,,/Fonts/#CODE Light)�61   $��Light"�6g   $��20(�6z   $��10 0 0 0q�6�   $��Account info��6�   ���#CFD8DC$��Center=�6�   $��Center=�6�   �� 5�      -   5        $
 accountImage��5�   %   $��1��6;   $��Center=�6H   $��Center=�6c   .+�5�      �� -    5        $+ btnUpdateImage��5�   %   $	��120��6=   $	��120��6I   #��[ 6V   $( Change profile image��6�   ��5�   "   �� 5�   &   -!   5        AccountImage��$ Aliased=�5�   y   $ HighQuality=�6�    6G   �� *CurrentUserVM.Model.Image+6�   5�      5�      �� 5�      $��2��6$   $.��(pack://application:,,,/Fonts/#CODE Light)�61   $��Light"�6g   $��20(�6z   $��10 0 0 0q�6�   $��Account options��6�   ���#CFD8DC$��Center=�6�   $��Center=�6�   �� 5�      $��3��6%   $	 Vertical=�62   $��Center=�6I   $	��Top=�6f   .+�5�      �� $	 
Horizontal=�6)   $��10q�6B   .+�5�   "     $  Account=�6:   $��Bottom=�6�   $	��0 4q�6�   ��6I   �� $$ userInfo��$% BorderBrush�6�   �� 5�   "   -"   5        $& userInfo��5�   *   $	��220��6<   $' 	User_name��6H   $��2 0 0 0q�6s   {�.  ��6�   �� *CurrentUserVM.Model.UserName+5�      �� 5�      $	 
Horizontal=�6)   $��10q�6B   .+�5�   "     $	  Key=�6:   $��Bottom=�6�   $	��0 4q�6�   ��6E   �� $$ passwordInfo��$% BorderBrush�6�   G� 5�   "   -#   5        $) passwordInfo��5�   .   $	��220��6D   $' Password��6P   $��2 0 0 0q�6z   6�   5�      �� 5�      $	 
Horizontal=�6)   $��10q�6B   .+�5�   "     $	  Key=�6:   $��Bottom=�6�   $	��0 4q�6�   ��6E   �� $$ rePasswordInfo��$% BorderBrush�6�   G� 5�   "   -$   5        $) rePasswordInfo��5�   .   $	��220��6F   $' Re-Password��6R   $��2 0 0 0q�6   6�   5�      �� 5�      $	 
Horizontal=�6)   $��10q�6B   $��Center=�6N   $��Center=�6i   .+�5�   "   �� -%   5        $+ 	btnUpdate��5�   )   $��Update��6<   $	��100��6M   $��Center=�6Y   $��Center=�6v   ���#CFD8DC���#263238���#CFD8DC{�.  6�   5�      5�      �� 5�      $��4��6$   $.��(pack://application:,,,/Fonts/#CODE Light)�61   $��Light"�6g   $��20(�6z   $��10 0 0 0q�6�   $��Account log out��6�   ���#CFD8DC$��Center=�6�   $��Center=�6�   �� 5�      $��5��6%   $	 Vertical=�62   .+�5�      �� -&   5        $+ 	btnLogOut��5�   %   $	��100��68   ���#CFD8DC$��10q�6Y   .��5�   "     $  
AccountOff=�6:   $��Center=�6L   $��Center=�6g   
������ ���#263238���#CFD8DC6�   5�      5�      5�      5�      5�      � 5�      -'   5        $! Messages��5�      $��1��6%   ���#37474F$��
-175 0 0 0q�6J    5�      �� 5�      $��* �6$   �� 5�      $
��Auto �6$   .+�5�      �� -(   5        0 ��Name$0 MessageList��5�      $��0��60   $��30q�6=   |�.  $��0q�6�   $��Stretch=�6�   $��Hidden=�6�   $��Hidden=�6  ��6I   �� *.,CurrentUserVM.CurrentConversationVM.Messages+��5�      �� 5�      " ��6    .�5�      ��  � {� .  6:   5�      5�      �� 5�      $��1��6   $��5 0 5 5q�6*   
������ .+�5�      � -)   5        $! grdMessageControl��5�      $y�	Collapsed=�66   - 5�      �� 5�      $��* �6/   �� 5�      $��50 �6/   .+�5�      �� -*   5        $& 	tbMessage��5�   "   $��0��65   $
��Auto��6E   $��2 0 0 0q�6S   1 �� TextWrapping$1 WrapWithOverflow=�6e   $��
10 0 10 10q�6�   
������ ���#CFD8DC$��Stretch=�6�   2 �� AcceptsReturn2 .  ��6�   �� *
Model.Text+.��6  3 J� InputBindings3 5�      �� 5�   "   
4 �� Key$4 Return��6-   5 �� Command#5 [ 6:   .��5�   5   5�      �� 5�      -+   5        $+ SendMessage��5�   !   $��1��66   $��30��6F   $��30��6Q   
������ $��0q�6v   
������ $��5q�6�   ��6�   �� $$ 	tbMessage��$% BorderBrush���6�   (
   "Direct_Response.Utility.ComBinding
  *0.CurrentUserVM.CurrentConversationVM.AddMessage+��67  �� $$ 	tbMessage��$
% Text�{�6u  �� $$ 	tbMessage��$% Text.Length�.��5�        $
  Send=�66   $��30��6B   $��30��6M   
������ $��5q�6s   5�      5�      5�      5�      5�      5      5     