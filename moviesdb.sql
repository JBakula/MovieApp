PGDMP                         {           moviesdb    15.2    15.2 K    e           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
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
    public          postgres    false    218   �^       X          0    16447    CategoryMovies 
   TABLE DATA           V   COPY public."CategoryMovies" ("CategoryMovieId", "CategoryId", "MovieId") FROM stdin;
    public          postgres    false    224   1_       T          0    16421 	   Directors 
   TABLE DATA           C   COPY public."Directors" ("DirectorId", "DirectorName") FROM stdin;
    public          postgres    false    220   �_       Z          0    16463    Images 
   TABLE DATA           E   COPY public."Images" ("ImageId", "ImageName", "MovieId") FROM stdin;
    public          postgres    false    226   a       \          0    16476    MovieActors 
   TABLE DATA           U   COPY public."MovieActors" ("MovieActorId", "ActorId", "MovieId", "Star") FROM stdin;
    public          postgres    false    228   Pk       V          0    16429    Movies 
   TABLE DATA           {   COPY public."Movies" ("MovieId", "MovieName", "Year", "CoverImage", "Description", "DirectorId", "IMDbRating") FROM stdin;
    public          postgres    false    222   �l       `          0    16507    Ratings 
   TABLE DATA           M   COPY public."Ratings" ("RatingId", "MovieId", "Value", "UserId") FROM stdin;
    public          postgres    false    232   �y       b          0    16545    RefreshTokens 
   TABLE DATA           h   COPY public."RefreshTokens" ("RefreshTokenId", "Token", "CreatedAt", "ExpiresAt", "UserId") FROM stdin;
    public          postgres    false    234   z       ^          0    16499    Users 
   TABLE DATA           h   COPY public."Users" ("UserId", "Name", "Lastname", "Email", "PasswordHash", "PasswordSalt") FROM stdin;
    public          postgres    false    230   ��       N          0    16399    __EFMigrationsHistory 
   TABLE DATA           R   COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
    public          postgres    false    214   ��       i           0    0    Actors_ActorId_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public."Actors_ActorId_seq"', 88, true);
          public          postgres    false    215            j           0    0    Categories_CategoryId_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public."Categories_CategoryId_seq"', 14, true);
          public          postgres    false    217            k           0    0 "   CategoryMovies_CategoryMovieId_seq    SEQUENCE SET     T   SELECT pg_catalog.setval('public."CategoryMovies_CategoryMovieId_seq"', 126, true);
          public          postgres    false    223            l           0    0    Directors_DirectorId_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('public."Directors_DirectorId_seq"', 19, true);
          public          postgres    false    219            m           0    0    Images_ImageId_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."Images_ImageId_seq"', 131, true);
          public          postgres    false    225            n           0    0    MovieActors_MovieActorId_seq    SEQUENCE SET     N   SELECT pg_catalog.setval('public."MovieActors_MovieActorId_seq"', 200, true);
          public          postgres    false    227            o           0    0    Movies_MovieId_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public."Movies_MovieId_seq"', 48, true);
          public          postgres    false    221            p           0    0    Rating_RatingId_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public."Rating_RatingId_seq"', 260, true);
          public          postgres    false    231            q           0    0     RefreshTokens_RefreshTokenId_seq    SEQUENCE SET     R   SELECT pg_catalog.setval('public."RefreshTokens_RefreshTokenId_seq"', 218, true);
          public          postgres    false    233            r           0    0    Users_UserId_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public."Users_UserId_seq"', 19, true);
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
       public          postgres    false    230    234    3247            P   �  x�=TKr�6]�O��K�SKKɱ���DU�lZT[DHʖ�S�by��,	6�ם��x�����Y�k0�2�l���N/�
��v<����Sǎ
���}#��	�J�|Џf�R�iY�^�qB�z���yGs��;}���S2Sk��b�$Q�V?rc��$EǱ�3��f���(�4Jr�m8XA�;߲��xZ�g�i�3� ���ztӍJM͸�[>X	� �G�\�	�\mb���.��{qz�;�������������T-��N?�ᵽP��n�w��;WK�B�����R
�ؾ�zeĹئT���s೷SZ�-w#$�\M�O`j�k���C��ο�-e3�ݚ?��;�AY�~���{�Ve����^�� ��G��,;����w��P��k���x���� �Rmx�`����~06�"�`��5h�y��:&���P6Wk�a��� ���
l�'Q8Oy2ebk>��YK��z�gz�!)���Gy}���'U�sps���b)/?�K��+�|��h��j�C���k���z�����b��Do�GE���],�PT���EC��"SO1�@��B����Ȩ�����/�iZy�)KL�	�(J5)w�C�o��J�Iߛ���E?��"�Os��ܭ<��0�Fy��⨜}�gc���ߨL�wS��;���0{$n�v��2��Q��-\�d�\ş`���c���PИ�=F��Q�^�f��.ܷ�����U���Q���f<.TΣ�m�~��������a�c�řw��;G� �R������HW�,Uq����j��"T_�7c�=U%��iN�S�'l�
�^&1U��p9�Y��OTE�����4P=�:p��0�8�%j+{�\��L߰E�꘎� ���JG�Ο���ꌽL�g:�X�Q��~_^��^��w}��� ���GW-�����&i	������4��      R   �   x�˻�@�z�h�W	Cc�&46��&�V��{�;�aj5ȘQ�}2?�b(�S35TT�kĖ�\;�eIb{��2Ԛ�#��:{��N�35^��^S��%�opI�����&��zgx�|2)�      X   �   x�%��D!�3�#�
�l�ul�=�F4���l�wڨn�%��������֪�;Q[�{,��e�B�u|�gP73��?8[��<���S��E�ZZ(G�FRK� EKQ���8Nj��FM��j��Dj;�-�
(�!��Q�z�i�j�K6�G.�z	�V�cK����D|v^���q�J)=�      T      x�-��j1E��W�B�w�y�Ő���n��Șx��vR���n���5PgyJ��w�o8��;I�-3j�	�$K���{*<��kZOp;~:K_�%��mt)kߖĻz��jT�h}��%\��u�y�\T+�t��@u�9�y�#��c�$h\T�@k�%��>r�ӎ#_5��jgCq����C�Kr.7�E�������M�?�����D�K��hi�}_�E���ZK��y���f�_Ǒ�M�E���c�1��~����hx      Z   8
  x�eW�RI}n��˝����m�h$ڶl1���J�� �i~c~o�dNI���ƄC�:y+�=�5��?����2%��J�D�3|Y�x���1��z7��������g|��������������`7x���7�W^��,ǿI~b��A�$iƔ�B�'TT%:i�7FFYs-�J��� ��@��}�E8�)�-
�OI�9����l08��������hk��f���z���������}���y������.�wz8�]>~�Q��un��u�pKw�������C)����"��O������:�P����{����t�S�-7�b~x#qb�+��ĝ�I	�)8��a�K��{��ǩ�r�X�������1S��)	H�`k_�b�����Ќ')�+�b)%]�����9+��F)#s�ȗ�����#��C��L\��t=���	yb��AIj�T�H*q἗��	��������
y���U(M�ؼ�ʾ��Li�B+ə$S���b�T�B��>���|�W/����
��Eb��!��b��qG�!�D�V�x��S�Wպj���b�Lpƅd �P&�؞p>��aD��I�d4����[MuP��`��Y�qD/e�'T s�Ф=^Pp�Ra�	W��/�-V^ݷ�GY�gW<(���b����%g�6����bp:�קo�����q�|��c&&g�=W'��K���Q�P
��	�A�p��&ow�ɗ��y��� �<V��?���S4\"LY�%���QrC���4�W/���.M�9
��WFC��P)�D��/��ĬX.|�)o��y�(��>W5���(�����2'��b�c��xf�-D�.V��
k0�w��>�����Z��r1T�qq*)nr6���r�H���U>n��mwU�����cs�57V�_f�=���Y�ʚ�f!t!3Q�'�؏������d~�U�����?�����r��t:;8�@��	�s�gٲT��f�<u2X-��}�
`��*^�$��Es�N�dxt6�R6�*K�U>�+	6xN���'� I��HC�y��Xx�*Y�L���-�����oг��������r�O��BR�k��ˉ�b�� ��æ��������=���h�!�@rF&���4y㸴���}���8ק���-�g}z��vy?<���v0�]ף�ٙ����г��<�7_w��(��������'�y�3�<.�k3.�	��Q�@�y%O;<ݓ��^;V����K�3�x�z��{�a�-����,�f;��}�W��F�T���'�'�OQ+b��R�p�����eu|l�Tu%���WT�1/M⁣>�1��0%i. �fR��1<�3
V�\0b)�t��O�G�^�3}��O�����A��`2���ۜ\l�������v8�`?u�맅���~9��K�j/�,�Q~Z�n��\�͞�O��WxD9Ǡ&$m#�N��
�#H'S�������\I���m����U֙r]6��(%KN2�d��9ڇ���"�s��|}��T���ʁ��O�,)j�x�df
"e�=��$���G��h��E�=UJ$T@�@��M�4"�5f�~���ɖ�K�z<�N�Q���0.��h��������u��.��r;�CIP=[�B���LgI�j�\�YHx���N��b;��Hh]��]��d�j�".]��3�B��6���dt����P*d9�@S�-��IA��v\?6)W0{��jd�d`D^r؉E��Jp��L1Vk�(�p���=�:Ցι��'.q�:x\�N�:[���W��:o��H�qA�KH���4FQKD��P�}��?}�Q��"�P0�w^%b�e�!�!�p2x��`��/���-�k�ֺ�I=cT��Eh@��M1Η���}a�1g����B���<�[MS�~�(�,�sa�,���g����1�a��^Q�I!�B �i�E�Ǭe��1g`ɂ�a���33E9|)�VQ�sV�/E�}(����?�
m�v|_��>p\��_�+h==����đ��v��6{h��eR	-�T]|�/��dO)P�Q/(d5�Ooc
2'qF�i��ѣ=�:W���m]E$s���#�J@"�R���	��ۭ,+��xrhf2I��ɫ���#�Y��u�9FDt˛�6���.�lB���U]�*��M�}�{�~⥱�ˑX�OQz�)�:`�8ء<.ڇ�4����3��6��ٷ���J������j��D㾙Uiۥ��m����'/l��0���zi���M/���������-���z�)vX,��(VB�Y��;^�a��2r"�'zCu�=��2�oO�O�	x�!�w���������6����{��- � �o���3�!��8��=��No����M�l���[��p6���Xܝ6��3u�킆�[~�m�����.������A>^U����?I�;�]1�{����g$h��Q�l����ux4�y���B���O"Wɣu�9��2�
6F��� �he�����M��u��	��[��W��}�s#=���$��y*�䂨��ys�֫	���|���oNNN��k�      \   �  x�=�A�$!�u�a扢�]f='������w?�PȒ�q��?o^دB��1��5�l��	Y�;m��+#��'����l_��~�V�b4-�q�}s�Z_��v��ޘ�fg�`F�e��?v�y8[8��6H3����ҧ�]�����b9Q&��<��>��U�q7����|��v����ˁ�Z�z�7e�����m��_�y��>�w�_o���_�Yv�/�����g����1�Q;������y���`���mN�@�_pG��;���xNc��<���i��`��ޚ��A4<�s2�+j��+_��l�.uAL���..D.+�`��˖٬�Pq�<��/�B�Eo�M���=C�6�Y��Sv�����r���+؜�h�5���2=�8��������e��      V   �  x�uX�R�H|6_Qo���}hc�Cc`���T*Y�u��-��)�=�D�Ѳ-�t��d摩O�d6�����L~�K������p3v�i��9�o%qے�Ot3y�2�=^y�ޮ&Wi'�Y��U."Od՝�nW��â��_r^�,����Z��*�+&����9�y��u5����}����!/���q�.�qV�E��X�Z1�.�����,��j���vy�$��Ks9q&ޥ}f
����ڌW�]&��vX~���u����ˑB�kz��OM�-#q�������ƜO�>:]dD��7M˰xx���i�-�/��50��l��F��67�_�ۿ�]"�z���.�>����o�o/��m \ߞ��5M{S�Q��C�}�JV����y�ɛv[��{.���uSд�e��Y�=��rC ᜶.8��x��xU���Ys�7k�~�aSZl���-o	�ˉnL�K��4'׼+yŮ�*�Zb�}N�D�s;u$���8����M�z���#p�EdE� ��- �\��a�2��ML��X��ã��n�	\�x܇��~ ��Ƽ�����ey�J�Y7���Աzj�K�����67y)/�<��x�K�-��Z�@�iz�H& aM�ȗ����ێHd~��v-��R���kI�~J-鹚#����N��o���/T!3�^6x֋Z�~�˄��������Z7E�^y�f�s�Pҷ]�sZ����?��VO�.�p�l�	B6V]�r�����_	ɸ�V�?�Wް-�T�^�$/C~�[pc⟙��Ͼز[P�KC�ضR��ua��E�2ܷ'��cC݅Y0����.\\�\ӂ�w4��:0f]txɃ��fv�.�_���G�����E����=�.e���8'�������V��4 M���T��O�m9���I^�_��·�TJ�R�(�x1��=�Y�X����� .G=�C�$��p�P��a��w��M�������[��HϷ�ᦉe�w5�j �Lu�wR�k_��q��H-��=�Å��n�r�u�8}K�M���ã�"U\A�k<Q!y��i$!0@����{�}SW�ޔuV��s�r�ۦNj��:���v����I����:6�q�ja�׼����D4W!p�~O4�O�p͖��$~�V�x����Y��
�r��f���|�1狫.�>jD���e��:�H~�b�7�!\�a=E�&�W�~��G��~��k&��I�FI�V؀Y`A�*I�f�ʚ��ԍ1ćڭ����s�J��5T_A�ڬJһ���E�;�t榪w��x�,x\7�	|l���vڟL�l.��EOU�M�t�Lo����6�;��}ણ�ԖkԌD[X���RZ`�n�~��|���:���
^g ^�I�߳�EVF�]�n�.�6a�����L7v�x�7������E��5	g2�jQP^u6,mK�?_>]2�`����D:�4"A�}��^�C���� ���-�aVÂ�FE�vL��bd�)�q�q�蜙�䮮� ���N�<�cG�'=�o�=�uS�{�4lǱ߶5i���t�I�Z��@��.+����$K� ���s�xC�Я� %�O$�c�����$ʇ����B��j�uƢ�/��%`�;� ~H{W%�)�3e���,.�◥�J�P'0��k���)|�քg;��g`���0R�Ф)��Ѐ�>,�;0i*���_e��+��|�K��_��_��xmI]xC	R�L����y,**�"�tڪ�Nޱ:���6%�`��PA����r4D��7*�����A@с��&��8�tD�����A�4Xt����@K82���؃�Ɣ�vL6�ų���Bc��?i���f����z�{��r�Ά�t�^��Ƙ���X'��(O��%T��8�IN����X��b����:rF�@Vm0��"���)���Y�d�����,G&��'u��23��]���q�vRW��+]S�G�gC��֯�9}.�k^]W�"%v��J�����u�Ì2Y(1�ǔ��.��6�\�o	��	D+�0���Ҋ^~6��CXt�~^tc@�w�2 P�#�o�͏��:�L���T���$��KǸf薮J6��)=�v��������b�~�>\���"Ђ�o?�q�˔4$yA�n��� 6T��n��m�VLt��-%B�YU�j �Z!��W�n��n�*] t1Tr
Z�(j$�
�o�7���s[B�8�b\�0w,�A� W���N���J�$o9i�H.�̲&�5�0�Li�{���v
1��3cQ��R�k�����=c-�\7Z��������N�זy��Q93�E4|���ib��5���U�eH�����#qZh��Y�J.�p����;�:�2���X�#����Hd�2�^���ܑk6����ʦ��?�c�gI#�x�1�x�s���Jl���yL_J${�{#�R�w��a��(�G�=b֣9�{����_����ٞ�?��1P^�i�����(m�	_�hT	~�CR7Q6?�n���J�P�SZW���T��fa�>/h�ĉ��2z��X9��J$����zP;��a��i��<P�=�!�璱),A��'��M��,g�P�}{�s�4e�"Fp�ۖ4y;��Q���@���APv�B�.��~�d4i������狛!8�g����k�:6X��-�(��}Gҽ���G{�0�$�$o0{�=�Yt�*|��7g'	BU�1s�w��N%�Kv���Q�~�`�I[���[����Zlu+r�����s6ι�T�qȅd�$]bRf+,�����Tv��	iwr�P������wcS�^j��-+NM>���H�L���G�g���/�E�a�ͣp7;E�5���}>Ǵ߿�=�n�p}���dNs�C�~�Yd@����O;Ҟ��W�:�dF+�������$��s/t�Sk�i�'xZ�| �h��!�;kf-囟���N"]�O�cXn����.�4q�ej��,G��0�/��Ʋ��3�)�
�Z�~փ�j�\��p�����L0LQ��g�e�ذ����۳�'�E�*hXh��*e�$��P��M#˚�B��S��}�{яw��,�-��IQ�[!N�����"�Sa�Ʊ��c�c��Q*`��#8g��<;;�1rF�      `   j   x�-�A!��1[J�ѿ��߱�bu�t�x@s:xp�-d����qa�S<�!.;*�q�2^Nq��W���Z�֔�Ɣp6]�3<��2Tì�&��z����hC\      b   �  x�m�I��ʺ@Ǟ_q��U2I �w@'(� 
J�	=�(����9u���8�k���O����P�Sq��vjiX��
z���IiY�.��xdWE��Z�$��h�a6��R�d&bҎ��l��k�p���7�$�KP�� ���E�s���?�eP�@`�SEG��W�e������A((�N2pu�������ۇ��)�9D��Вu�km4��1����|�1_�^ �4��)f~����D/�I�nF��Lv��^:��w�ju���Z��#�K�D�Ya�E�z�T����-a����RZ��{:|�A���̂ �%�`�/D��9+�M����5׽Tޓ�|��`q�	�ZaSQw�+ڝ
�ORK�L�S	f�<����i$�d����4C�I.�"�T�EI�S�vG�6�~�Ox<6I06��n��g�B�G�����v��Kk������rӑj �r�j�B64r���Wg�S�p�`1��($�S���n+�E�(8��L��1v���T6_�$qy����C�j��"��>RyҼH�SԜA�]qYK��=_*?���_�Z0����T�E�S��=6�� E +��XU9R�t��𔪛ԭ�P�aɳ��uj*����T�m�e��g�����F =�Û5��D�,��j�7J�?��, �|M��Z�ȃ
W:���Y�抟���-q��p\鬅靜�~���Z3��
"�>�󝗞���|4�Ƽ�B������(`'7v�r�kȍ~Q��D��aO���������@]a����!�l��Ws��mV�+���'[�T�pc?�o�6}�&��D,~�k�wʲ���"f�&H~��ưi2��F�;��W�ά��qP��T���K��Ӽ��)s���7�a������
L��^l�M�,E��{�O�
f�ڕe�3u(�v2O���*�O����b�P�0�A^�uD���F�T�*���vV�7��X��9�܆d�p�x���`�O6�;E?�gk�\ݶN��ˍxKE K�	��p�"EGk�|o�z.nc�j�b.y����c�puK�����F�j��T#����B��: �S�o�ș�Z���l�w�V�b�ɚD>\K<��#���ʗb+�7����t��w���̟�-'��Mj���/j�8�@4�鏌~�W����@W�D��ԨK]��Q/s>�۵�*�W�Fo/=eL��� �SuU��|�]�x��J�Ş�*�J9��%�j��dX�#��tR��٫XK�x�$�Juz�9�,2�X"��.ַg��2�w���\��*��+?솣���̤"���m��� ~!�ED�O6�I�I��]�T3ﾰR�J˘�����s^4�;^6'4���)^`M̀��S4�Y��mc��
5��f(��љ��j�Ч�Y�}E��Q#�($�6��s$��yKrK�w.fy�X�]���ʉ�@!gE���i�@n�qӘуm�8�X���-�O��$�45����������vݬWO,�h�������2F��Ő�V'��}������SO.w����5a�Sh��I�2� ��u�Vk�H��,�F3~R����_����� ㉫n�����LyٲV��� ���@�Ź�mS��{`�^	�����CT���bl:�!���A#�W�?N2 P���M�t�����闒$���Ȭ�~_�G�F*��cq��&�����RĖR�#���c��4�����*�Q@��M�%Y��~�!8m������|M�_���f�Xc)1C���a�c�5�智���EY�c�7 �o��8d4_\��9����|��~�7}��g�]�����/b�ӵ8�X$������N_j�L�ς�:��h�1't!ʢ�����ܮ�WF�[s\+���1��)�Ul��Zե��������O5 ~�d��������"���7�Sֆ�Ȗ̛��;WF�07��md|�/��7Ξ6��x(Bc�����QIb�CǦKa(��|낿ը�������O�_��7j�,Qxv>l�\�s�[nۇ�H���F�	LT��kɓ�U���q>�+�`���r+�{ĵ9����5�5}�x�,K�w��)�yx��/�L{m	��$�ZM%x��r).G���_��<�V�x��?�������]zz�e�����t�4y
?so��yk�i��<�����]퓾�ؙ?١�ףT�O��E���K]3�>C۟Gb��V��h(����G��`��ƬQ3�ur��Ǜc�Bk�K�+�qjSN��AO����O:�b�7���
����υ�Qd�����=0zٝ�����.���(��ی�-�X�H����4��(|O(|�/ I�~�IS3�Xлr>�Wkw�y�������tAs���z@I��y��pT���������MӅ����bC��}P�92��@��i�N�kN����᢭<��GB�I���$�8�)�9�٧o;���C�օp�sKlʄ�Qw��̫��sr�b���_]@�8z HD���O��$Ivf�ig���!9��3�J�Β�2����rS��A�i����h*�e2����̵{g��K���?D�/�zU-X�77�BQ��
=�և �f���p�0Ɩ5���$c�
��T�̣�e�{��:;W�O:U�8�m%ˢB����Ɏ�[��|��eꋠC ������7�J)`f}˫m�e|۞9�r��}�<�L�=|
��M�ݓ��J�J�+�Hv2I��p���@��srB��U��77���Ğ�6-p����/H2?��g�S�1í���ڌ�X�1���6Κ�E�D�Us1ږ|8-����0Ǒ?�����߯����v=��=��MA��=�2}_T�~E�NQ���_���R]�      ^   �  x�US9�\7�gN���� ����L	��lKr����o�w���X�n~�V_�=~�Z_�����O�Ϗ��O��597!@�nZ`�Qľ �qw.\%�\��.\z�Oq(��k�u^b�b��K�-u�yG���Ԡ��U�_:��V�B6m�� P<M�@�k���y�ڽ}�]v��86\��+�n�)VWMvA���:7YW�����e��E���M߃�݉�繊56N�93�ZK�l�-���v"{��Ei��x�#�-/���a��U�E��p��y}�R��x����р�:;��԰v��Nb�83�T�9�6h���4�˖��70@��:]2E$��˖-��y�'�l@�u���p*yH1�5�s/��]��5��]:ek��Exe��l��y [�5!5	G2&b1ߓه�ԥ��r|2���ivji��"W!�4���#�F�:�d�ؓq���J��C�]3�@���
O[,���5𺣧v�6�wm�lz�=~�������g���[9G�6�����㙭���VY���UO	6�����W���1�R}�@I�1-���_�C�zD)#�ф��h�6>A��K`�UH�B���]�Ʃz�e����Q�������yT�,���G���ٝ�d����KǃB&�{�u�$��[N�L�ƃ��^��r�A'��N�b�����vK��5�����fiyi�xɉ���8R}&~x�|>�8.c	      N   �   x�uнN�0�<�� :����Z�!Y�����.зǁ24m=����>	��-,�c������d��ᮩ���L�R����G:n?\�.��F�f�-]�s)�6��mҋ�kd)��˯�,�������U��Z�fj�lS���6v�)рVJHc�c~t�߶3�¢\J�5��;��Qox.@Bkb����)#�Ro6L!d�+��Y�֚�aڳ�1�L9�T�0��ُ�g�~�����փa     