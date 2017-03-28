IF NOT EXISTS (SELECT 1 FROM T_BAS_FILTERSCHEME WHERE FSCHEMEID='58c8d9be1da2fe')
BEGIN
/****** Object:Data       Script Date: 2017-03-22 14:04:27 ******/
DELETE T_BAS_FILTERSCHEME WHERE FSCHEMEID='58c8d9be1da2fe' 
INSERT INTO T_BAS_FILTERSCHEME(FSCHEMEID,FFORMID,FSCHEMENAME,FUSERID,FISDEFAULT,FSCHEME,FISSHARE,FNEXTENTRYSCHEME,FSEQ) VALUES ('58c8d9be1da2fe','SEC_PermissionItem',N'业务单据',16394,'0',null,0,'0',22)  

/****** Object:Data       Script Date: 2017-03-22 14:04:27 ******/
DELETE T_BAS_FILTERSCHEME_L WHERE FSCHEMEID='58c8d9be1da2fe' 
INSERT INTO T_BAS_FILTERSCHEME_L(FPKID,FSCHEMEID,FLOCALEID,FDESCRIPTION) VALUES ('58c8d9be1da2ff','58c8d9be1da2fe',2052,N'业务单据')  

END;
