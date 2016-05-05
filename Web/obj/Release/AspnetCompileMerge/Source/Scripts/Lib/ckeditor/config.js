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
    config.disableObjectResizing = true;
    config.forcePasteAsPlainText = false;//不去除
    config.allowedContent = true;
    config.font_names = '新細明體;細明體;標楷體;微軟正黑體;Arial;AArial Black;Comic Sans MS;Courier New;Tahoma;Times New Roman;Verdana;';
    config.skin = 'kama';
    config.resize_dir = 'vertical';
    config.keystrokes = [
       [CKEDITOR.ALT + 121 /*F10*/, 'toolbarFocus'], //獲取焦點
        [CKEDITOR.ALT + 122 /*F11*/, 'elementsPathFocus'], //元素焦點
       [CKEDITOR.SHIFT + 121 /*F10*/, 'contextMenu'], //文本功能表
       [CKEDITOR.CTRL + 90 /*Z*/, 'undo'], //撤銷
        [CKEDITOR.CTRL + 89 /*Y*/, 'redo'], //重做
        [CKEDITOR.CTRL + CKEDITOR.SHIFT + 90 /*Z*/, 'redo'], //
        [CKEDITOR.CTRL + 76 /*L*/, 'link'], //鏈結
        [CKEDITOR.CTRL + 66 /*B*/, 'bold'], //粗體
        [CKEDITOR.CTRL + 73 /*I*/, 'italic'], //斜體
        [CKEDITOR.CTRL + 85 /*U*/, 'underline'], //下劃線
        [CKEDITOR.ALT + 109 /*-*/, 'toolbarCollapse']
    ]
    //設置快捷鍵 可能與流覽器快捷鍵衝突 plugins/keystrokes/plugin.js.
    config.blockedKeystrokes = [
        CKEDITOR.CTRL + 66 /*B*/,
        CKEDITOR.CTRL + 73 /*I*/,
        CKEDITOR.CTRL + 85 /*U*/
    ]
    config.toolbar =
    [
        ['Undo', '-', 'Redo', '-', 'Cut', '-', 'Copy', '-', 'Paste', '-', 'PasteText', '-', 'Image', '-', 'Table', '-', 'Templates', '-', 'HorizontalRule', , '-', 'PageBreak', '-', 'SpecialChar', '-', 'Bold', '-', 'Italic', '-', 'Strike', '-', 'TextColor', '-', 'BGColor', '-', 'NumberedList', '-', 'BulletedList', '-', 'Outdent', '-', 'Indent', '-', 'Link', '-', 'Unlink'],
        '/',
        ['Styles', 'Format', 'Font', 'FontSize', 'JustifyLeft', '-', 'JustifyCenter', '-', 'JustifyRight', '-', 'JustifyBlock'], ['Preview', '-', 'Maximize']
    ];
};

