===============================================
サンプルデータをお使いになる前にお読みください
===============================================

●サンプルファイルについて

サンプルファイルには『Unity5オンラインゲーム開発講座 クラウドエンジンによるマルチプレイ＆課金対応ゲームの作り方』の該当するLESSONで利用するサンプルプログラムを収録しています。
該当するLESSONの「before」フォルダには、実習で利用する完成前のサンプルプログラムが、「after」フォルダには実習を通じて作った完成サンプルプログラムが入っています。
お使いのコンピュータにコピーしてお使いください。

●免責事項について

サンプルファイルに収録されたファイルは通常の運用においては何ら問題ないことを編集部では確認していますが、万一運用の結果、いかなる損害が発生したとしても著者及び株式会社翔泳社はいかなる責任も負いません。すべて自己責任においてご使用ください。

●サンプルプログラムの動作環境
　本書のサンプルは、以下の環境で正常に動作することを確認しています。サンプルプログラムの実機およびバージョン対応についてはP.009を参照してください。

開発環境

OS
・Windows 8/7（64bit）

CHAPTER 01 ～ CHAPTER 10
・Unity 5.0.2
・appc cloud SDK 3.0.1
・Kii Cloud SDK 2.9.0Kii Cloud SDK 2.9.0
・Photon Unity Networking Free 1.51
・UnityAds 1.2.0

CHAPTER 11
・Unity 5.1.1



●サンプルファイルの構成
サンプルファイルのフォルダ構成は次の通りです。

UNITY5 ONLINE【サンプルファイルのトップフォルダ】

    +-- README.txt【サンプルファイルの内容に関して説明しているテキスト（本ファイル）】

    +-- CHAPTER 03【CHAPTER 03のフォルダ】

	+-- LESSON 03【LESSON 03のフォルダ】

            +--before【LESSON 03の完成前サンプルプログラムの入ったフォルダ】
            	+--photonSample【LESSON 03の完成前サンプルプログラム】

            +--after【LESSON 03の完成後サンプルプログラムの入ったフォルダ】
            	+--photonSample【LESSON 03の完成後サンプルプログラム】

    +-- CHAPTER 04【CHAPTER 04のフォルダ】

	+-- chapter04【該当するLESSONで利用するパッケージファイル】
            +--UIbase.unitypackage【LESSON 03で利用するパッケージファイル】
            +--objects.unitypackage【LESSON 04で利用するパッケージファイル】

	+-- LESSON 01【LESSON 01のフォルダ】

            +--after【LESSON 01の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 01の完成後サンプルプログラム】

	+-- LESSON 02【LESSON 02のフォルダ】

            +--before【LESSON 02の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 01の完成前サンプルプログラム】

            +--after【LESSON 02の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 02の完成後サンプルプログラム】

	+-- LESSON 03【LESSON 03のフォルダ】

            +--before【LESSON 03の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 03の完成前サンプルプログラム】

            +--after【LESSON 03の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 03の完成後サンプルプログラム】

	+-- LESSON 04【LESSON 04のフォルダ】

            +--before【LESSON 04の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 04の完成前サンプルプログラム】

            +--after【LESSON 04の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 04の完成後サンプルプログラム】

    +-- CHAPTER 05【CHAPTER 05のフォルダ】

	+-- chapter05【該当するLESSONで利用するパッケージファイル】
            +--shell.unitypackage【LESSON 04で利用するパッケージファイル】
            +--smoke.unitypackage【LESSON 07で利用するパッケージファイル】

	+-- LESSON 01【LESSON 01のフォルダ】

            +--before【LESSON 01の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 01の完成前サンプルプログラム】

            +--after【LESSON 01の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 01の完成後サンプルプログラム】

	+-- LESSON 02【LESSON 02のフォルダ】

            +--before【LESSON 02の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 02の完成前サンプルプログラム】

            +--after【LESSON 02の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 02の完成後サンプルプログラム】

	+-- LESSON 03【LESSON 03のフォルダ】

            +--before【LESSON 03の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 03の完成前サンプルプログラム】

            +--after【LESSON 03の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 03の完成後サンプルプログラム】

	+-- LESSON 04【LESSON 04のフォルダ】

            +--before【LESSON 04の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 04の完成前サンプルプログラム】

            +--after【LESSON 04の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 04の完成後サンプルプログラム】

	+-- LESSON 05【LESSON 05のフォルダ】

            +--before【LESSON 05の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 05の完成前サンプルプログラム】

            +--after【LESSON 05の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 05の完成後サンプルプログラム】

	+-- LESSON 06【LESSON 06のフォルダ】

            +--before【LESSON 06の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 06の完成前サンプルプログラム】

            +--after【LESSON 06の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 06の完成後サンプルプログラム】

	+-- LESSON 07【LESSON 07のフォルダ】

            +--before【LESSON 07の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 07の完成前サンプルプログラム】

            +--after【LESSON 07の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 07の完成後サンプルプログラム】

    +-- CHAPTER 06【CHAPTER 06のフォルダ】

	+-- chapter06【該当するLESSONで利用するパッケージファイル】
            +--rader.unitypackage【LESSON 03で利用するパッケージファイル】

	+-- LESSON 01【LESSON 01のフォルダ】

            +--before【LESSON 01の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 01の完成前サンプルプログラム】

            +--after【LESSON 01の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 01の完成後サンプルプログラム】

	+-- LESSON 02【LESSON 02のフォルダ】

            +--before【LESSON 02の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 02の完成前サンプルプログラム】

            +--after【LESSON 02の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 02の完成後サンプルプログラム】

	+-- LESSON 03【LESSON 03のフォルダ】

            +--before【LESSON 03の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 03の完成前サンプルプログラム】

            +--after【LESSON 03の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 03の完成後サンプルプログラム】

	+-- LESSON 04【LESSON 04のフォルダ】

            +--before【LESSON 04の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 04の完成前サンプルプログラム】

            +--after【LESSON 04の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 04の完成後サンプルプログラム】

	+-- LESSON 05【LESSON 05のフォルダ】

            +--before【LESSON 05の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 05の完成前サンプルプログラム】

            +--after【LESSON 05の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 05の完成後サンプルプログラム】

    +-- CHAPTER 07【CHAPTER 07のフォルダ】

	+-- chapter07【該当するLESSONで利用するパッケージファイル】
            +--UIstart.unitypackage【LESSON 03で利用するパッケージファイル】
            +--script.unitypackage【LESSON 03で利用するパッケージファイル】

	+-- LESSON 01【LESSON 01のフォルダ】

            +--before【LESSON 01の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 01の完成前サンプルプログラム】

            +--after【LESSON 01の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 01の完成後サンプルプログラム】

	+-- LESSON 02【LESSON 02のフォルダ】

            +--before【LESSON 02の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 02の完成前サンプルプログラム】

            +--after【LESSON 02の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 02の完成後サンプルプログラム】

	+-- LESSON 03【LESSON 03のフォルダ】

            +--before【LESSON 03の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 03の完成前サンプルプログラム】

            +--after【LESSON 03の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 03の完成後サンプルプログラム】

    +-- CHAPTER 08【CHAPTER 08のフォルダ】

	+-- LESSON 01【LESSON 01のフォルダ】

            +--before【LESSON 01の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 01の完成前サンプルプログラム】

            +--after【LESSON 01の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 01の完成後サンプルプログラム】

	+-- LESSON 02【LESSON 02のフォルダ】

            +--before【LESSON 02の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 02の完成前サンプルプログラム】

            +--after【LESSON 02の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 02の完成後サンプルプログラム】

	+-- LESSON 03【LESSON 03のフォルダ】

            +--before【LESSON 03の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 03の完成前サンプルプログラム】

            +--after【LESSON 03の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 03の完成後サンプルプログラム】

    +-- CHAPTER 09【CHAPTER 09のフォルダ】

	+-- chapter09【該当するLESSONで利用するパッケージファイル】
            +--obj2.unitypackage【LESSON 02で利用するパッケージファイル】

	+-- LESSON 01【LESSON 01のフォルダ】

            +--before【LESSON 01の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 01の完成前サンプルプログラム】

            +--after【LESSON 01の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 01の完成後サンプルプログラム】

	+-- LESSON 02【LESSON 02のフォルダ】

            +--before【LESSON 02の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 02の完成前サンプルプログラム】

            +--after【LESSON 02の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 02の完成後サンプルプログラム】

	+-- LESSON 03【LESSON 03のフォルダ】

            +--before【LESSON 03の完成前サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 03の完成前サンプルプログラム】

            +--after【LESSON 03の完成後サンプルプログラムの入ったフォルダ】
            	+--project_SS【LESSON 03の完成後サンプルプログラム】

    +-- CHAPTER 11【CHAPTER 11のフォルダ】

	+-- LESSON 02【LESSON 02のフォルダ】

            +--after【LESSON 02の完成後サンプルプログラムの入ったフォルダ】
            	+--UMPsample【LESSON 02の完成後サンプルプログラム】

	+-- LESSON 03【LESSON 03のフォルダ】

            +--before【LESSON 03の完成前サンプルプログラムの入ったフォルダ】
            	+--UMPsample【LESSON 03の完成前サンプルプログラム】

            +--after【LESSON 03の完成後サンプルプログラムの入ったフォルダ】
            	+--UMPsample【LESSON 03の完成後サンプルプログラム】


●著作権等について

　本書に収録したソースコード、オブジェクトデータの著作権は、著者および株式会社翔泳社が所有しています。個人で使用する以外に利用することはできせん。また許可なくネットワークなどへの配布もできません。個人的に使用する場合においては、ソースコードの改変や流用は自由です。商用利用に関しては、株式会社翔泳社へご一報ください。

※本書に記載された URL 等は予告なく変更される場合があります。

※本書の出版にあたっては正確な記述につとめましたが、著者や出版社などのいずれも、本書の内容に対してなんらかの保証をするものではなく、内容やサンプルに基づくいかなる運用結果に関してもいっさいの責任を負いません。

※本書に掲載されているサンプルプログラムやスクリプト、および実行結果を記した画面イメージなどは、特定の設定に基づいた環境にて再現される一例です。

※そのほか本書に記載されている会社名、製品名はそれぞれ各社の商標および登録商標です。

※本書の内容は、2015 年 7 月執筆時点のものです。

※そのほか本書に記載されている会社名、製品名はそれぞれ各社の商標および登録商標です。

2015年9月　株式会社翔泳社　編集部
