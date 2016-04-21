/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.pasteFromWordRemoveFontStyles = false;
    config.pasteFromWordRemoveStyles = false;
    config.forcePasteAsPlainText = false;//不去除
    config.skin = 'kama';
    config.resize_dir = 'vertical';
    config.toolbar =
    [
        ['Undo', '-', 'Redo', '-', 'Cut', '-', 'Copy', '-', 'Paste', '-', 'PasteText', '-', 'Image', '-', 'Table', '-', 'Templates', '-', 'HorizontalRule', , '-', 'PageBreak', '-', 'SpecialChar', '-', 'Bold', '-', 'Italic', '-', 'Strike', '-', 'TextColor', '-', 'BGColor', '-', 'NumberedList', '-', 'BulletedList', '-', 'Outdent', '-', 'Indent', '-', 'Link', '-', 'Unlink'],
        '/',
        ['Styles', 'Format', 'Font', 'FontSize', 'JustifyLeft', '-', 'JustifyCenter', '-', 'JustifyRight', '-', 'JustifyBlock'], ['Preview', '-', 'Maximize']
    ];
};

