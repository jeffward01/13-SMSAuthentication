   M S B A M L   `   `   `  ����O  KContactManagementApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ContactManagementAppContactManagementApp  5   	        ContactManagementApp.Login   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ContactManagementApp     5   	     � Title$  Login��5   	   $	��300��6   $��463.855��6$   .��5	      � $��0,0,2,0q�6   .+�5
   
   �� -   5         ��Name$ label��5
      $5��/Please enter your phone number to Authenticate ��6   $
��Left=�6Y   $��30��6t   $��	24,39,0,0q�6�   $	��Top=�6�   $	��288��6�    J� RenderTransformOrigin$ 0.425,0.499&�6�   �� 5   
   -   5        $ label1��5      $9��3An authentication text  (code) will be sent to you.��6    $
��Left=�6^   $��	22,69,0,0q�6y   $	��Top=�6�   $ -0.358,-0.66&�6�   �� 5   
   -   5        $ label2��5      $��Cell Phone Number:��6    $
��Left=�6=   $��27��6X   $��
24,127,0,0q�6d   $	��Top=�6x   $	��116��6�   �� 5   
   -   5         ��Name$$ loginWindow_textBox_EnterPhone��5      $
��Left=�6:   $��23��6U   $��145,131,0,0q�6a    �� TextWrapping$
 Wrap=�6v   $��XXX-XXX-XXXX��6�   $	��Top=�6�   $��90��6�   6�   �� 5   
   -   5         ��Name$! loginWindow_button_SendText��5      $��	Send Text��66   $
��Left=�6J   $��27��6e   $��252,126,0,0q�6q   $	��Top=�6�   $��72��6�   $ 2.654,3.462&�6�   6�   �� 5   
   -   5        $ label2_Copy��5      $��Verification Code:��6%   $
��Left=�6B   $��27��6]   $��
22,178,0,0q�6i   $	��Top=�6}   $	��104��6�   �� 5   
   -   5        $* $loginWindow_textBox_VerificationCode��5      $
��Left=�6@   $��27��6[   $��131,182,0,0q�6g   $
 Wrap=�6|   $��Enter Code Here��6�   $	��Top=�6�   $	��104��6�   6�   �� 5   
   -   5        $# loginWindow_button_SubmitCode��5      $��Submit Code��68   $
��Left=�6N   $��27��6i   $��252,182,0,0q�6u   $	��Top=�6�   $��87��6�   $ 2.654,3.462&�6�   6�   �� 5   
   -	   5        $ label3��5      $.��(To Resend Verification Code Click here: ��6    $
��Left=�6S   $��37��6n   $��
10,222,0,0q�6z   $	��Top=�6�   $	��213��6�   �� 5   
   -
   5        $# loginWindow_button_ReSendText��5      $��Resend Code��68   $
��Left=�6N   $��27��6i   $��238,222,0,0q�6u   $	��Top=�6�   $	��101��6�   $ 2.654,3.462&�6�   6�   �� 5   
   -   5        $  loginWindow_button_CallUse��5      $��Call w/ Code ��65   $
��Left=�6M   $��27��6h   $��346,126,0,0q�6t   $	��Top=�6�   $��85��6�   $ 2.654,3.462&�6�   6�   5      5      