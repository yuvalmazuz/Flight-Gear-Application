   M S B A M L   `   `   `  ����  flightSimulator&     flightSimulator.controlPanelView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$  Control Panel��5      $	��250��6   $	��750��6&   ��5      �� 5	   
    �� ImageSource$L FC:\Users\User\source\repos\flightSimulator\flightSimulator\cockpit.jpg��6    j� Stretch$
 Fill=�6j   .��5      �� $	��400��6   $	��700��6   $��Center=�6*   .+�5   
   �� -   5         ��Name$ pathTextBox��5   M   $	��200��6   $��20��6   $��18q�6*   $��Choose a file..��66   6b   �� 5   
   -   5         ��Name$ 
LoadButton��5      $��Right=�6>    J� RenderTransformOrigin$ 5.078,3.056&�6Z   $
��Load��6~   $��50, -10, 250, 1q�6�   �� 5   
   -   5        $ BrowseButton��5      $
��Left=�6B   $ 5.078,3.056&�6]   $��Browse��6�   $��250, -21, 50, 10q�6�   �� 5   
   -   5        $ 
PlayButton��5      $
��Left=�6>   $ 5.078,3.056&�6Y   $
��Play��6}   $��230, -20, 0, 0q�6�   $y�Hidden=�6�   �� 5   
   -   5        $ 
StopButton��5      $��Right=�6>   $ 5.078,3.056&�6Z   $
��Stop��6~   $��-10, -20, 240, 0q�6�   $y�Hidden=�6�   �� 5   
   -   5        $ PauseButton��5      $��Center=�6@   $ 5.078,3.056&�6]   $��Pause��6�   $��-13, 10q�6�   $y�Hidden=�6�   �� 5   
   -   5        $��speedComboBox��5      $
��0, 0q�6(   $	��100��66    �� 
IsEditable .  �� 
IsReadOnly .  �� Text$ Select Speed��6g   $y�Hidden=�6�   �� 5   
   -   5        $��Slider��5      	 �� TickFrequency$	 1Q�6   
�������$��0, 10q�6g   
 � Minimum$
 0Q�6�   $y�Hidden=�6�    � Value 62   �� *	VM_Time+ � Maximum 6v   �� *VM_FlightLength+6�   5      5      