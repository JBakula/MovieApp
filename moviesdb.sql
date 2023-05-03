PGDMP                         {           moviesdb    15.2    15.2 K    e           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            f           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            g           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            h           1262    16398    moviesdb    DATABASE     �   CREATE DATABASE moviesdb WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United Kingdom.1252';
    DROP DATABASE moviesdb;
                postgres    false            �            1259    16405    Actors    TABLE     `   CREATE TABLE public."Actors" (
    "ActorId" integer NOT NULL,
    "ActorName" text NOT NULL
);
    DROP TABLE public."Actors";
       public         heap    postgres    false            �            1259    16404    Actors_ActorId_seq    SEQUENCE     �   ALTER TABLE public."Actors" ALTER COLUMN "ActorId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Actors_ActorId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    216            �            1259    16413 
   Categories    TABLE     j   CREATE TABLE public."Categories" (
    "CategoryId" integer NOT NULL,
    "CategoryName" text NOT NULL
);
     DROP TABLE public."Categories";
       public         heap    postgres    false            �            1259    16412    Categories_CategoryId_seq    SEQUENCE     �   ALTER TABLE public."Categories" ALTER COLUMN "CategoryId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Categories_CategoryId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    218            �            1259    16447    CategoryMovies    TABLE     �   CREATE TABLE public."CategoryMovies" (
    "CategoryMovieId" integer NOT NULL,
    "CategoryId" integer NOT NULL,
    "MovieId" integer NOT NULL
);
 $   DROP TABLE public."CategoryMovies";
       public         heap    postgres    false            �            1259    16446 "   CategoryMovies_CategoryMovieId_seq    SEQUENCE     �   ALTER TABLE public."CategoryMovies" ALTER COLUMN "CategoryMovieId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."CategoryMovies_CategoryMovieId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    224            �            1259    16421 	   Directors    TABLE     i   CREATE TABLE public."Directors" (
    "DirectorId" integer NOT NULL,
    "DirectorName" text NOT NULL
);
    DROP TABLE public."Directors";
       public         heap    postgres    false            �            1259    16420    Directors_DirectorId_seq    SEQUENCE     �   ALTER TABLE public."Directors" ALTER COLUMN "DirectorId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Directors_DirectorId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    220            �            1259    16463    Images    TABLE     �   CREATE TABLE public."Images" (
    "ImageId" integer NOT NULL,
    "ImageName" text NOT NULL,
    "MovieId" integer NOT NULL
);
    DROP TABLE public."Images";
       public         heap    postgres    false            �            1259    16462    Images_ImageId_seq    SEQUENCE     �   ALTER TABLE public."Images" ALTER COLUMN "ImageId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Images_ImageId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    226            �            1259    16476    MovieActors    TABLE     �   CREATE TABLE public."MovieActors" (
    "MovieActorId" integer NOT NULL,
    "ActorId" integer NOT NULL,
    "MovieId" integer NOT NULL,
    "Star" boolean
);
 !   DROP TABLE public."MovieActors";
       public         heap    postgres    false            �            1259    16475    MovieActors_MovieActorId_seq    SEQUENCE     �   ALTER TABLE public."MovieActors" ALTER COLUMN "MovieActorId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."MovieActors_MovieActorId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    228            �            1259    16429    Movies    TABLE     �   CREATE TABLE public."Movies" (
    "MovieId" integer NOT NULL,
    "MovieName" text NOT NULL,
    "Year" integer NOT NULL,
    "CoverImage" text NOT NULL,
    "Description" text,
    "DirectorId" integer NOT NULL,
    "IMDbRating" real
);
    DROP TABLE public."Movies";
       public         heap    postgres    false            �            1259    16428    Movies_MovieId_seq    SEQUENCE     �   ALTER TABLE public."Movies" ALTER COLUMN "MovieId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Movies_MovieId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    222            �            1259    16507    Ratings    TABLE     �   CREATE TABLE public."Ratings" (
    "RatingId" integer NOT NULL,
    "MovieId" integer NOT NULL,
    "Value" integer NOT NULL,
    "UserId" integer DEFAULT 0 NOT NULL
);
    DROP TABLE public."Ratings";
       public         heap    postgres    false            �            1259    16506    Rating_RatingId_seq    SEQUENCE     �   ALTER TABLE public."Ratings" ALTER COLUMN "RatingId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Rating_RatingId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    232            �            1259    16545    RefreshTokens    TABLE     �   CREATE TABLE public."RefreshTokens" (
    "RefreshTokenId" integer NOT NULL,
    "Token" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "ExpiresAt" timestamp with time zone NOT NULL,
    "UserId" integer NOT NULL
);
 #   DROP TABLE public."RefreshTokens";
       public         heap    postgres    false            �            1259    16544     RefreshTokens_RefreshTokenId_seq    SEQUENCE     �   ALTER TABLE public."RefreshTokens" ALTER COLUMN "RefreshTokenId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."RefreshTokens_RefreshTokenId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    234            �            1259    16499    Users    TABLE     �   CREATE TABLE public."Users" (
    "UserId" integer NOT NULL,
    "Name" text NOT NULL,
    "Lastname" text NOT NULL,
    "Email" text NOT NULL,
    "PasswordHash" bytea NOT NULL,
    "PasswordSalt" bytea DEFAULT '\x'::bytea NOT NULL
);
    DROP TABLE public."Users";
       public         heap    postgres    false            �            1259    16498    Users_UserId_seq    SEQUENCE     �   ALTER TABLE public."Users" ALTER COLUMN "UserId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Users_UserId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    230            �            1259    16399    __EFMigrationsHistory    TABLE     �   CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);
 +   DROP TABLE public."__EFMigrationsHistory";
       public         heap    postgres    false            P          0    16405    Actors 
   TABLE DATA           :   COPY public."Actors" ("ActorId", "ActorName") FROM stdin;
    public          postgres    false    216   �Z       R          0    16413 
   Categories 
   TABLE DATA           D   COPY public."Categories" ("CategoryId", "CategoryName") FROM stdin;
    public          postgres    false    218   �]       X          0    16447    CategoryMovies 
   TABLE DATA           V   COPY public."CategoryMovies" ("CategoryMovieId", "CategoryId", "MovieId") FROM stdin;
    public          postgres    false    224   a^       T          0    16421 	   Directors 
   TABLE DATA           C   COPY public."Directors" ("DirectorId", "DirectorName") FROM stdin;
    public          postgres    false    220   �^       Z          0    16463    Images 
   TABLE DATA           E   COPY public."Images" ("ImageId", "ImageName", "MovieId") FROM stdin;
    public          postgres    false    226   �_       \          0    16476    MovieActors 
   TABLE DATA           U   COPY public."MovieActors" ("MovieActorId", "ActorId", "MovieId", "Star") FROM stdin;
    public          postgres    false    228   `g       V          0    16429    Movies 
   TABLE DATA           {   COPY public."Movies" ("MovieId", "MovieName", "Year", "CoverImage", "Description", "DirectorId", "IMDbRating") FROM stdin;
    public          postgres    false    222   �h       `          0    16507    Ratings 
   TABLE DATA           M   COPY public."Ratings" ("RatingId", "MovieId", "Value", "UserId") FROM stdin;
    public          postgres    false    232   �q       b          0    16545    RefreshTokens 
   TABLE DATA           h   COPY public."RefreshTokens" ("RefreshTokenId", "Token", "CreatedAt", "ExpiresAt", "UserId") FROM stdin;
    public          postgres    false    234   �q       ^          0    16499    Users 
   TABLE DATA           h   COPY public."Users" ("UserId", "Name", "Lastname", "Email", "PasswordHash", "PasswordSalt") FROM stdin;
    public          postgres    false    230   �u       N          0    16399    __EFMigrationsHistory 
   TABLE DATA           R   COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
    public          postgres    false    214   w       i           0    0    Actors_ActorId_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public."Actors_ActorId_seq"', 69, true);
          public          postgres    false    215            j           0    0    Categories_CategoryId_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public."Categories_CategoryId_seq"', 14, true);
          public          postgres    false    217            k           0    0 "   CategoryMovies_CategoryMovieId_seq    SEQUENCE SET     T   SELECT pg_catalog.setval('public."CategoryMovies_CategoryMovieId_seq"', 112, true);
          public          postgres    false    223            l           0    0    Directors_DirectorId_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('public."Directors_DirectorId_seq"', 16, true);
          public          postgres    false    219            m           0    0    Images_ImageId_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."Images_ImageId_seq"', 116, true);
          public          postgres    false    225            n           0    0    MovieActors_MovieActorId_seq    SEQUENCE SET     N   SELECT pg_catalog.setval('public."MovieActors_MovieActorId_seq"', 176, true);
          public          postgres    false    227            o           0    0    Movies_MovieId_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public."Movies_MovieId_seq"', 43, true);
          public          postgres    false    221            p           0    0    Rating_RatingId_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."Rating_RatingId_seq"', 55, true);
          public          postgres    false    231            q           0    0     RefreshTokens_RefreshTokenId_seq    SEQUENCE SET     Q   SELECT pg_catalog.setval('public."RefreshTokens_RefreshTokenId_seq"', 11, true);
          public          postgres    false    233            r           0    0    Users_UserId_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."Users_UserId_seq"', 3, true);
          public          postgres    false    229            �           2606    16411    Actors PK_Actors 
   CONSTRAINT     Y   ALTER TABLE ONLY public."Actors"
    ADD CONSTRAINT "PK_Actors" PRIMARY KEY ("ActorId");
 >   ALTER TABLE ONLY public."Actors" DROP CONSTRAINT "PK_Actors";
       public            postgres    false    216            �           2606    16419    Categories PK_Categories 
   CONSTRAINT     d   ALTER TABLE ONLY public."Categories"
    ADD CONSTRAINT "PK_Categories" PRIMARY KEY ("CategoryId");
 F   ALTER TABLE ONLY public."Categories" DROP CONSTRAINT "PK_Categories";
       public            postgres    false    218            �           2606    16451     CategoryMovies PK_CategoryMovies 
   CONSTRAINT     q   ALTER TABLE ONLY public."CategoryMovies"
    ADD CONSTRAINT "PK_CategoryMovies" PRIMARY KEY ("CategoryMovieId");
 N   ALTER TABLE ONLY public."CategoryMovies" DROP CONSTRAINT "PK_CategoryMovies";
       public            postgres    false    224            �           2606    16427    Directors PK_Directors 
   CONSTRAINT     b   ALTER TABLE ONLY public."Directors"
    ADD CONSTRAINT "PK_Directors" PRIMARY KEY ("DirectorId");
 D   ALTER TABLE ONLY public."Directors" DROP CONSTRAINT "PK_Directors";
       public            postgres    false    220            �           2606    16469    Images PK_Images 
   CONSTRAINT     Y   ALTER TABLE ONLY public."Images"
    ADD CONSTRAINT "PK_Images" PRIMARY KEY ("ImageId");
 >   ALTER TABLE ONLY public."Images" DROP CONSTRAINT "PK_Images";
       public            postgres    false    226            �           2606    16480    MovieActors PK_MovieActors 
   CONSTRAINT     h   ALTER TABLE ONLY public."MovieActors"
    ADD CONSTRAINT "PK_MovieActors" PRIMARY KEY ("MovieActorId");
 H   ALTER TABLE ONLY public."MovieActors" DROP CONSTRAINT "PK_MovieActors";
       public            postgres    false    228            �           2606    16435    Movies PK_Movies 
   CONSTRAINT     Y   ALTER TABLE ONLY public."Movies"
    ADD CONSTRAINT "PK_Movies" PRIMARY KEY ("MovieId");
 >   ALTER TABLE ONLY public."Movies" DROP CONSTRAINT "PK_Movies";
       public            postgres    false    222            �           2606    16525    Ratings PK_Ratings 
   CONSTRAINT     \   ALTER TABLE ONLY public."Ratings"
    ADD CONSTRAINT "PK_Ratings" PRIMARY KEY ("RatingId");
 @   ALTER TABLE ONLY public."Ratings" DROP CONSTRAINT "PK_Ratings";
       public            postgres    false    232            �           2606    16551    RefreshTokens PK_RefreshTokens 
   CONSTRAINT     n   ALTER TABLE ONLY public."RefreshTokens"
    ADD CONSTRAINT "PK_RefreshTokens" PRIMARY KEY ("RefreshTokenId");
 L   ALTER TABLE ONLY public."RefreshTokens" DROP CONSTRAINT "PK_RefreshTokens";
       public            postgres    false    234            �           2606    16505    Users PK_Users 
   CONSTRAINT     V   ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("UserId");
 <   ALTER TABLE ONLY public."Users" DROP CONSTRAINT "PK_Users";
       public            postgres    false    230            �           2606    16403 .   __EFMigrationsHistory PK___EFMigrationsHistory 
   CONSTRAINT     {   ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");
 \   ALTER TABLE ONLY public."__EFMigrationsHistory" DROP CONSTRAINT "PK___EFMigrationsHistory";
       public            postgres    false    214            �           1259    16491    IX_CategoryMovies_CategoryId    INDEX     c   CREATE INDEX "IX_CategoryMovies_CategoryId" ON public."CategoryMovies" USING btree ("CategoryId");
 2   DROP INDEX public."IX_CategoryMovies_CategoryId";
       public            postgres    false    224            �           1259    16492    IX_CategoryMovies_MovieId    INDEX     ]   CREATE INDEX "IX_CategoryMovies_MovieId" ON public."CategoryMovies" USING btree ("MovieId");
 /   DROP INDEX public."IX_CategoryMovies_MovieId";
       public            postgres    false    224            �           1259    16493    IX_Images_MovieId    INDEX     M   CREATE INDEX "IX_Images_MovieId" ON public."Images" USING btree ("MovieId");
 '   DROP INDEX public."IX_Images_MovieId";
       public            postgres    false    226            �           1259    16494    IX_MovieActors_ActorId    INDEX     W   CREATE INDEX "IX_MovieActors_ActorId" ON public."MovieActors" USING btree ("ActorId");
 ,   DROP INDEX public."IX_MovieActors_ActorId";
       public            postgres    false    228            �           1259    16495    IX_MovieActors_MovieId    INDEX     W   CREATE INDEX "IX_MovieActors_MovieId" ON public."MovieActors" USING btree ("MovieId");
 ,   DROP INDEX public."IX_MovieActors_MovieId";
       public            postgres    false    228            �           1259    16497    IX_Movies_DirectorId    INDEX     S   CREATE INDEX "IX_Movies_DirectorId" ON public."Movies" USING btree ("DirectorId");
 *   DROP INDEX public."IX_Movies_DirectorId";
       public            postgres    false    222            �           1259    16522    IX_Ratings_MovieId    INDEX     O   CREATE INDEX "IX_Ratings_MovieId" ON public."Ratings" USING btree ("MovieId");
 (   DROP INDEX public."IX_Ratings_MovieId";
       public            postgres    false    232            �           1259    16538    IX_Ratings_UserId    INDEX     M   CREATE INDEX "IX_Ratings_UserId" ON public."Ratings" USING btree ("UserId");
 '   DROP INDEX public."IX_Ratings_UserId";
       public            postgres    false    232            �           1259    16557    IX_RefreshTokens_UserId    INDEX     Y   CREATE INDEX "IX_RefreshTokens_UserId" ON public."RefreshTokens" USING btree ("UserId");
 -   DROP INDEX public."IX_RefreshTokens_UserId";
       public            postgres    false    234            �           2606    16452 6   CategoryMovies FK_CategoryMovies_Categories_CategoryId    FK CONSTRAINT     �   ALTER TABLE ONLY public."CategoryMovies"
    ADD CONSTRAINT "FK_CategoryMovies_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES public."Categories"("CategoryId") ON DELETE CASCADE;
 d   ALTER TABLE ONLY public."CategoryMovies" DROP CONSTRAINT "FK_CategoryMovies_Categories_CategoryId";
       public          postgres    false    224    3229    218            �           2606    16457 /   CategoryMovies FK_CategoryMovies_Movies_MovieId    FK CONSTRAINT     �   ALTER TABLE ONLY public."CategoryMovies"
    ADD CONSTRAINT "FK_CategoryMovies_Movies_MovieId" FOREIGN KEY ("MovieId") REFERENCES public."Movies"("MovieId") ON DELETE CASCADE;
 ]   ALTER TABLE ONLY public."CategoryMovies" DROP CONSTRAINT "FK_CategoryMovies_Movies_MovieId";
       public          postgres    false    222    224    3234            �           2606    16470    Images FK_Images_Movies_MovieId    FK CONSTRAINT     �   ALTER TABLE ONLY public."Images"
    ADD CONSTRAINT "FK_Images_Movies_MovieId" FOREIGN KEY ("MovieId") REFERENCES public."Movies"("MovieId") ON DELETE CASCADE;
 M   ALTER TABLE ONLY public."Images" DROP CONSTRAINT "FK_Images_Movies_MovieId";
       public          postgres    false    222    226    3234            �           2606    16481 )   MovieActors FK_MovieActors_Actors_ActorId    FK CONSTRAINT     �   ALTER TABLE ONLY public."MovieActors"
    ADD CONSTRAINT "FK_MovieActors_Actors_ActorId" FOREIGN KEY ("ActorId") REFERENCES public."Actors"("ActorId") ON DELETE CASCADE;
 W   ALTER TABLE ONLY public."MovieActors" DROP CONSTRAINT "FK_MovieActors_Actors_ActorId";
       public          postgres    false    216    228    3227            �           2606    16486 )   MovieActors FK_MovieActors_Movies_MovieId    FK CONSTRAINT     �   ALTER TABLE ONLY public."MovieActors"
    ADD CONSTRAINT "FK_MovieActors_Movies_MovieId" FOREIGN KEY ("MovieId") REFERENCES public."Movies"("MovieId") ON DELETE CASCADE;
 W   ALTER TABLE ONLY public."MovieActors" DROP CONSTRAINT "FK_MovieActors_Movies_MovieId";
       public          postgres    false    228    222    3234            �           2606    16441 %   Movies FK_Movies_Directors_DirectorId    FK CONSTRAINT     �   ALTER TABLE ONLY public."Movies"
    ADD CONSTRAINT "FK_Movies_Directors_DirectorId" FOREIGN KEY ("DirectorId") REFERENCES public."Directors"("DirectorId") ON DELETE CASCADE;
 S   ALTER TABLE ONLY public."Movies" DROP CONSTRAINT "FK_Movies_Directors_DirectorId";
       public          postgres    false    220    3231    222            �           2606    16526 !   Ratings FK_Ratings_Movies_MovieId    FK CONSTRAINT     �   ALTER TABLE ONLY public."Ratings"
    ADD CONSTRAINT "FK_Ratings_Movies_MovieId" FOREIGN KEY ("MovieId") REFERENCES public."Movies"("MovieId") ON DELETE CASCADE;
 O   ALTER TABLE ONLY public."Ratings" DROP CONSTRAINT "FK_Ratings_Movies_MovieId";
       public          postgres    false    3234    222    232            �           2606    16539    Ratings FK_Ratings_Users_UserId    FK CONSTRAINT     �   ALTER TABLE ONLY public."Ratings"
    ADD CONSTRAINT "FK_Ratings_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("UserId") ON DELETE CASCADE;
 M   ALTER TABLE ONLY public."Ratings" DROP CONSTRAINT "FK_Ratings_Users_UserId";
       public          postgres    false    232    3247    230            �           2606    16552 +   RefreshTokens FK_RefreshTokens_Users_UserId    FK CONSTRAINT     �   ALTER TABLE ONLY public."RefreshTokens"
    ADD CONSTRAINT "FK_RefreshTokens_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("UserId") ON DELETE CASCADE;
 Y   ALTER TABLE ONLY public."RefreshTokens" DROP CONSTRAINT "FK_RefreshTokens_Users_UserId";
       public          postgres    false    230    234    3247            P   �  x�=T�V�8]W}�~�9�3dIB'�C�ɦ7�S'V#K9�:|�\��YZ._�W9��/�̭]�)����h���7Kq�%ݍ���K�މ�^��ܪy�1pM�(�l����h�Vԙ�X�|M�2`T��yA��3w��{�f��x1?�!�f�8�,�����c�fo���-���,�S����v�D������O���΀����ٌ~�bN�tf'��3P���:qX�6<�&�|F��^��g� �Us\�=�9�&Ho�xj/����{�x.i%18a6!�9�a��Sk�V�O05A�7�Q�����vҍ�p{5�>����;1���|�%uh���wgIk������J��uN=9�c��`�vPߧ��~D'�/]%ܷ���m��G�z�����Q=Y۾}#�-�I[���2 �c�:5%���p���ъY�~���Z1#�d\�2�:���څ����E��5=���\̳�N	�}�ɕ��6��j��������r�/fk� O�kچS%��4�yA�2:��JW�P�w5��q������aW9݂�x�
U�����+���K������)�v����¶a�P���ɹ;Z���^��6bV-�@���?ͻ�O�n0�@؈5�����O}�vh������:�Z�����\�79���EJIL]Rz	��p<���+�ǂ�1��N��
�M�����9-|=O��2R=*��_;��D�U�*8�W��/v	f�      R   �   x�˻�@�z�h�W	Cc�&46��&�V��{�;�aj5ȘQ�}2?�b(�S35TT�kĖ�\;�eIb{��2Ԛ�#��:{��N�35^��^S��%�opI�����&��zgx�|2)�      X   �   x�%�� �o�'�
�t�9��_y;�Y���Q��J����2������:(�b��B�����>�R�X�p�y>jE�[��E�Z�#����Z�����
�jD=~'������*��Cb�<��q�@�*z      T   �   x���j�@E��W�L'����!����B7�-��㑙���;����k�NrO�d�5~p������%I���!f��Ŷ=��/��#�mG_�$��>ؘt�s�M{|���
�7�ǈ/p�#搲�>m�d��Ք�<��C���!J4n��B{�ģY�)��ȁ�����v>;��(�7�1�H���q(�-��3ݸ��7��_��H'�:My-~��� W�      Z   f  x�eWiS��l��x�����m��%[���ELT_b�.�@�_�)�� 1
uvugef��������/��N�z�k���^	�u�x9�����Q�L.�p�v�)���������͏�gw_�ǃ�`��߯ۯ�}=Y�_iy���FL���&����I����e�tҭ�*Ho�S��/ /� �����*�D�h�(�G��,�������`��㿧�F���zg�j7+�����ۋ���������n���|?�<�uo�p4;�}�4��u��s��p~��g����R�S)�$�J8�E�.!�H��T�h���wxϯW%��r�-�'RG^=��D2x��
�b�!KY�ʵ���4=N�Lx��OX$�k"j��S��
�9+I��[��2!���d�jퟰ�	��Nd��d(x���>i�D0N?�ĘΏOv�+��Ϟ8]���t?|��ț����.�H�L!�*0�⢲��2¶�73�7�������k��7l������h)4�8�j���8��G��O�yY=� �9aA�%��-�p̩&/d�֑�%�u���yZVͺ�v�M#T���B*-@�C�t�{�q�)$'������Y�w�{Km4�9z��2���L ���P+K�q@%9(�*lP���?�[�:���WY�WW�2��Hң��g9x��&��9�P��k}~�fw�����nxw����眛��k'd*匬�c��N�s@n�a�ɛ�`�e?��?�
������zuɜ�T�]F^�@Y�lR�ґ�ܒ����v�lk�z��ו�p'�L..d�5���̪���Oe�]͛Em6W�����_/�@�G�W�J��� ���Gӧ����G��T;]��/�0�P�s�����=p� �6���CEh�`���')Hn��E������t�o�<��lv]*���y�}�U����J�#[�����"TO.�_M��Go�~;|��'�� �����^rd��$�W!Č~ľ�E�"�3ެ�ۃ6�n:}�
`�ªlMV��ZI��N�9��ck��pXÑB͈�(KV��`��H{TEV/Kd�R�ɛ��2�]�(i���7�l��G$�xօ�ƙ���uD�V���d�"������4� Ҽ}v�4�������Z�0�1�Fd�]��'!���e�~\W'���;x�.� Ow��]��l��=}����D_^������!��˯��qR����o�*�E ^ƥK�}����*V����]7o���ս�@�gD-vH6F���c�zj�3İ���7]Y�fVr��}���Uy�ї͍�{�i�T圬!!��U�P��(�ܮW�;�㮛��P��az}%��`����y.�:����J�ד��W$��W���$r�!�k�L���ʹ���o6��f�N��dx��bsr��O0�Ϧ���T�`��_�]��~��7=��<M��2*s������&�`��g���b�>�$86;�:�\;.����ˍ�*�}=���C��(����8�S������3:�����YDp�J����f��=�J���өhJVY`���ʅ��k��e{����C��'$���L�D�~ �����͋�z�^lE��QOF>86�P#2����%��ly|�f��Yo�eu���ů�$�^-Z!��+�!���X����E�'h��v��А�iY��bl�h �b(�:�Ƚ�<�?Z�9&gs1�N�YN`�i����������u�4{��1kd� b-'\���6;�SjָQ����{�\G���LR�fmd\�ͅ�������l4�?�5
0d\,az�ɢ�ƈ@�R�g����2
�l�](:bρM&aEq����'�[(J&I��1�����ȯk�����_��?�I      \   1  x�=��q1�sO0.�@�\|v��a`oOR#4��X����#cÚ����="��"��V�GfBV�������ҙ�������J���)y��5����	��A��P��3���Ͷ'~��y9��"��ą433�[��e�K�μ�f9Q&N8��m_�('R���6\�'��#m��಼�eyãW��̦����a�<*+xT�_+����k���,�̶�>`�r�����k����1�Q[�{����Y��r0V���X�E���V��#�9�[�Ⲃ���h9v�]O�cb������<�)?��      V   	  x�uW�r�8|��o�bix����Q�ьIűlG��r� (R�-HY��~����Y�T9�@�Fw��k��dߴl�j-G�e�ߪ_���8ʹ�	��,˳/��,�|��綛�t�ܿϜl���U�I�8�4_W�q!�\6�%뎊�e;zR�eU��(�Zv��\5L6k�ۜe��j�:ń���6y�;l?��>���
�Z.tU`�f���}W�%����fu�U�$%Sx�'�`M��1 <��ؖ�ٲ�2��}��Gv{g0nI��)��n�Y����s�@�/ɓ�N���x[��n�,O�/��������*�&�j�T��'9��V�����k�c{�_q���O}��:��Ƿ�t�����~�I����۲���8�:�V2U����5�%�� �P�k%��J� h�+��mY�/1\n	 �iUͩTƛ���k������3x�V�u�Ciq�B���%�'#����uG׼��]�uմ�"��e���~H.=?b�ah�������拥��̜�p��:p����kw���%��>qV����_>�l��dq����S��_�6V���Z�C�J_��]gf��IQ�ˎSh	�u��c]�`1�o��}��O�Hh}0d��A�|y-��@��D����s|��E�x��g�S��(��fA�r�޾Ԙ=�Ǧ�^/5�:VB����7]������g��=s�f�Kh(?���8������+���9e�Ӕ-1AH�`U����<VmɎJoqKH��@��8�A^�f{�T���I�^��T���(�p�ѷC�g7��'A���
˵m���E���<������n�2��N��c�.��UeY��;u�I�Y�<=U�櫻<��dSo>	�=&�{������zE�P�e�2�݁��>�Mj�X�f�K�˱*���ٞ�~�ϫ#2ܬ:r5���4��
h�u���Z�Z6&���@����)�����;�n����w�t���_x@:��V2�=�E�y��CK�H%ۉC��z\c��*�tM�i�+����dc?��<�n�Y��� w���0\��vTK��=$�ȅ��:���j��w=)+��h�\��t�U�̚Po����Vd؉:4�*�4�w͛���%������oD���|��A��y;�G������hv�?�[w�����E4[-��w�3Ѝ짮W�t�<��t!�;���y�ٿ������������I���P�Y`A�K=�J.��/������Cr;4��G���Ҽj����X[*a,�Ӈ��)�߅���FA�a��y�4��}l�wE�	�j&�L�1Q����AUp�7�7=�Wz�7]��
�{n����F�Ʌ��q&�ڮg��E�6�6�&^�<� �fNN?�^�-���C�P����&ڤ�	��O�[?Y<�S��Qu���&�REƙ��$
�e�������Ä����V8%�I�	���qKg���"K���VjO~X*��L�H�iU7l��;�0#�5�7�*� ����uF�兟V,#A���0,x�����+���<��e-�YO��!\����-��j��KfxC��S-� @��?�ͱ��F![�l���?����#�͈��S�T��7�`~H{��)�3�RY��(X\,�/�2�ܢ6N`���Wıo��<.���iۅ��cIWf!VA�˷t�؁I������.}^�W���t鮌�o>��{2+�ړ�pM	�HFi��c�����(����I�G�DR��-;4\P���WA��r4LM��&����At���E��$/�³@���5���`�8a+a��%p内;h>�)��o�G��Rg�6�����v�l�H�7U�[m��Y?�����+AZ�:�r��$��ɷ=z�1*u��4�)\�JtD��s1d�uuF�@6�`�(����+��"Wp�9��f��c#J��
Ա3�E̐�m/�n�c?(B�]������G��������ysݠR��8Χk;�>z�����ͨ��1��Ґ б�qy�ù4�[�'mM�J�|��Ҋ���i�h�q�=x�])e ��B���~_���sA�w?��?L%��@x6>�۳m�É��8red��p6Wo�i���|�>]���"����?㲒y H�h��C{A(�T���6�o��J�/��[Ƅ:)�F�j�S�Z#��Q5�ݠ�ni���& ]|Tr
Zj05�k�����|r�2`K����s��wg�rJ4�b#Ew�5CV�&U��crE��\\\����      `   ,   x�35�46�4�4�26�46�4�L8��8-A, �,���� �]       b   �  x�m��΢X ��^E�M�8&�@FAIodP�����tM]�&�xx����q�e�_�t���yI���JF6���f�L�v����� �5�M�e��5.��S:���/�nC�������o���d��)a8��	@�+�g���x�F��v�F(��nB��YZ�0Q���m���RlNI}�Cz5�x���:��)D���Rn~�+��M����0=�̔4����`�1u�.������d����i��c��3�k�U�<Y.�\�*M��RXN�|����Y]����P�Ѻװ\���ض�c瀚��Ca�+�F�&��K钹�"kAd���C	�����շU�~�|�^{�A=�Mq�:$l��yh��.�W����p���N)���~����Ud��t�צ�tO�n���&�䱿��Z2����:6�w��꠮���<
WM{@*���Ɓ����Q\R���廊B�c�Bs�]��2?v�G9�A��h�ئ�u�-N��G�_M��@�鬼���p����e=���P�y�;�^�jf��+Fu�++i��w|ݚ�)�1����?*3#U5=a�䎨*y�V=�H�>s�P~0e�f��=A-/R7��,�l_��v�9ۅ��I�9n�67���j]T]�񉽖�ԇ�V<fGz{F\]R�,�,I�]lV�Y܃BiPzw���X��A[K>V���خ�Ѻ��Ɲ�屷r�z�t(��a���߀�̇7�k�cndBu�E yB�hm�ņ�V"6.� iRod駁����6��ڍ(曣5�*T�}k��cQ�� _ϔ)����0s������)U̲�a�<�6ew���N̷�����Lے����pض�C�pƞ]�x�z�޶��2t]���$���>���k�2z�rS�y����~��Hɰ6�u�̔�����Z�HACc��++��%]�������L�M�
c��b�]'�YQ���Z8_����)0e8�����k������_#��      ^     x���N@�뻇1�,t>�v�,,/&j��gG2��|y����w}]~���v_�O�y��n�>mmHP��B�;+�8@F"J�؊Z�K�[ъS9q�-Z��s�A|�a)�gYK��<��jF�����J�!�*�3 �ҁO�	�G�=q�^Y�1���F ��"2IK@Ohz-�)%K{�򮲢={YLq]�P��.?��,D��G�bM�C�S0��1�M4CezZ���{��K�Mвs7�(�� C���(�E�;���z{�^�z�x�      N   �   x�u��N�0�3}�<A�$u�h\v��6vC�R�um���'���>r�����D����r���Y���~`�}7m��J�2��0s�i��߼�R��Z��:[���Zk4w�m���Kd����We�0\q������y�>�R�e{h�`S����v<Q�l��uN�����۞�n�K�Y��ɉ��A[��/h�L�^0>e�c�نC�D���_A���a�2������㩮���y     